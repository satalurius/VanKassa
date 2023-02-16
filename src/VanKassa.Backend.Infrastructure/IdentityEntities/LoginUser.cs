using Microsoft.AspNetCore.Identity;

namespace VanKassa.Backend.Infrastructure.IdentityEntities;

public class LoginUser : IdentityUser<int>
{
    public IEnumerable<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}