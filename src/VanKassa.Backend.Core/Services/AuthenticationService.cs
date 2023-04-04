using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Backend.Infrastructure.IdentityEntities;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.Exceptions;
using VanKassa.Domain.Models.SettingsModels;
using VanKassa.Domain.ViewModels;
using VanKassa.Shared.Data;

namespace VanKassa.Backend.Core.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IDbContextFactory<VanKassaDbContext> dbContextFactory;
    private readonly UserManager<LoginUser> userManager;
    private readonly JWTSettings jwtSettings;

    public AuthenticationService(UserManager<LoginUser> userManager,
        IOptions<JWTSettings> jwtSettings, IDbContextFactory<VanKassaDbContext> dbContextFactory)
    {
        this.userManager = userManager;
        this.jwtSettings = jwtSettings.Value;
        this.dbContextFactory = dbContextFactory;
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<AuthenticateViewModel> AuthenticateAsync(AuthenticateDto authenticationDto)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var dbUser = await dbContext.Users
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(u => u.UserName == authenticationDto.Login);

        if (dbUser is null)
            throw new NotFoundException(AuthenticationErrors.EntityNotFound);

        var isPasswordCorrect = await userManager.CheckPasswordAsync(dbUser, authenticationDto.Password);

        if (!isPasswordCorrect)
            throw new ForbiddenException(AuthenticationErrors.LoginFailed);

        var userRoles = await userManager.GetRolesAsync(dbUser);

        var refreshToken = GenerateRefreshToken();

        refreshToken.UserId = dbUser.Id;

        await dbContext.AddAsync(refreshToken);

        var jwtToken = GenerateJwtToken(dbUser, userRoles);

        await RemoveOldRefreshTokensForUser(dbUser.Id);

        await dbContext.SaveChangesAsync();

        return new AuthenticateViewModel(jwtToken, refreshToken.Token);
    }

    public async Task RegisterAsync(RegisterDto registerDto)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        LoginUser? existUser = await userManager.FindByNameAsync(registerDto.UserName);

        if (existUser is not null)
        {
            throw new BadRequestException(AuthenticationErrors.EntityAlreadyExist);
        }

        var createUserOperation = await userManager.CreateAsync(
                new LoginUser
                {
                    UserName = registerDto.UserName
                },
                registerDto.Password)
            ;

        if (!createUserOperation.Succeeded)
        {
            throw new BadRequestException(AuthenticationErrors.FailRegister);
        }

        LoginUser createdUser = (await userManager.FindByNameAsync(registerDto.UserName))!;

        await userManager.AddToRoleAsync(createdUser, EnumConverters.ConvertRoleEnumToConstantValue(registerDto.Role));
    }


    /// <summary>
    /// Создает новый jwt и refresh токены.
    /// </summary>
    /// <param name="token"></param>
    public async Task<AuthenticateViewModel> RefreshTokenAsync(string token)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var user = await GetUserByRefreshTokenAsync(token);

        var refreshToken = user.RefreshTokens.First(rt => rt.Token == token);

        if (refreshToken.RevokedDate.HasValue)
        {
            RevokeDescendantRefreshTokens(refreshToken, user, $"Attempted reuse of revoke ancestor token: {token}");
        }

        if (DateTime.UtcNow >= refreshToken.ExpiredDate || refreshToken.RevokedDate.HasValue)
        {
            throw new BadRequestException(AuthenticationErrors.InvalidRefreshToken);
        }

        var newRefreshToken = RotateRefreshToken(refreshToken);

        newRefreshToken.UserId = user.Id;

        await dbContext.RefreshTokens.AddAsync(newRefreshToken);

        await RemoveOldRefreshTokensForUser(user.Id);

        await dbContext.SaveChangesAsync();

        var userRoles = await userManager.GetRolesAsync(user);

        var jwtToken = GenerateJwtToken(user, userRoles);

        return new AuthenticateViewModel(jwtToken, newRefreshToken.Token);
    }

    public async Task RemoveTokenAsync(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            throw new BadRequestException(AuthenticationErrors.TokenRequired);
        }

        var user = await GetUserByRefreshTokenAsync(token);

        var refreshToken = user.RefreshTokens.First(rt => rt.Token == token);

        if (DateTime.UtcNow >= refreshToken.RevokedDate || refreshToken.RevokedDate.HasValue)
        {
            throw new BadRequestException(AuthenticationErrors.InvalidRefreshToken);
        }

        RevokeRefreshToken(refreshToken, "Revoked without replacement", "");
    }

    private string GenerateJwtToken(LoginUser user, IEnumerable<string> userRoles)
    {
        var unixStartDate = new DateTime(1970, 1, 1);

        var notValidBeforeTime = (long)DateTime.UtcNow.AddSeconds(jwtSettings.AccessTokenExpireSeconds)
            .Subtract(unixStartDate).TotalDays;

        var expirationTime = (long)DateTime.UtcNow.AddSeconds(jwtSettings.AccessTokenExpireSeconds)
            .Subtract(unixStartDate).TotalSeconds;

        var payload = new JwtPayload
        {
            { JwtRegisteredClaimNames.Sub, user.UserName },
            { JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() },
            { JwtRegisteredClaimNames.Nbf, notValidBeforeTime },
            { JwtRegisteredClaimNames.Aud, jwtSettings.Audience },
            { JwtRegisteredClaimNames.Iss, jwtSettings.Issuer },
            { JwtRegisteredClaimNames.Exp, expirationTime },
            { CustomClaims.UserId, user.Id.ToString() },
            { CustomClaims.Roles, userRoles }
        };

        var jwtKey = Encoding.UTF8.GetBytes(jwtSettings.Secret);

        var key = new SymmetricSecurityKey(jwtKey);

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtHeader = new JwtHeader(creds);

        var securityToken = new JwtSecurityToken(jwtHeader, payload);

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.WriteToken(securityToken);

        return token;
    }

    private RefreshToken GenerateRefreshToken()
    {
        var expiredDate = DateTime.UtcNow.AddSeconds(jwtSettings.RefreshTokenExpireSeconds);

        var randomNumberGenerator = RandomNumberGenerator.Create();

        var randomBytes = new byte[64];

        randomNumberGenerator.GetBytes(randomBytes);

        return new RefreshToken
        {
            Token = Convert.ToBase64String(randomBytes),
            ExpiredDate = expiredDate,
            CreatedDate = DateTime.UtcNow
        };
    }

    private RefreshToken RotateRefreshToken(RefreshToken refreshToken)
    {
        var newRefreshToken = GenerateRefreshToken();

        RevokeRefreshToken(refreshToken, "Replaced by new token", newRefreshToken.Token);

        return newRefreshToken;
    }

    private static void RevokeRefreshToken(RefreshToken token, string reason, string replacedByToken)
    {
        token.RevokedDate = DateTime.UtcNow;
        token.RevokedReason = reason;
        token.ReplacedByToken = replacedByToken;
    }

    private async Task RemoveOldRefreshTokensForUser(int userId)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var date = DateTime.UtcNow.AddSeconds(-jwtSettings.RefreshTokenExpireSeconds);

        var refreshTokens = await dbContext.RefreshTokens
            .Where(rt => rt.UserId == userId &&
                         (DateTime.UtcNow >= rt.ExpiredDate && rt.RevokedDate.HasValue)
                         && rt.CreatedDate <= date)
            .ToListAsync();

        dbContext.RefreshTokens.RemoveRange(refreshTokens);
        await dbContext.SaveChangesAsync();
    }

    private async Task<LoginUser> GetUserByRefreshTokenAsync(string token)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var user = await dbContext.Users
            .Include(user => user.RefreshTokens)
            .FirstOrDefaultAsync(user => user.RefreshTokens.Any(rt => rt.Token == token));

        if (user is null)
        {
            throw new BadRequestException(AuthenticationErrors.InvalidRefreshToken);
        }

        return user;
    }

    private void RevokeDescendantRefreshTokens(RefreshToken refreshToken, LoginUser user, string reason)
    {
        if (string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            return;

        var childToken = user.RefreshTokens
            .FirstOrDefault(rt => rt.Token == refreshToken.ReplacedByToken);

        if (childToken is null)
            return;

        if (DateTime.UtcNow >= childToken.ExpiredDate && refreshToken.RevokedDate.HasValue)
        {
            RevokeDescendantRefreshTokens(childToken, user, reason);
        }
        else
        {
            RevokeRefreshToken(childToken, reason, "");
        }
    }
}