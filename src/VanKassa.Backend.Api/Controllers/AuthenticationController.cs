using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanKassa.Backend.Api.Controllers.Base;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.ViewModels;

namespace VanKassa.Backend.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/authentication")]
public class AuthenticationController : BaseController<IAuthenticationService>
{
    public AuthenticationController(IAuthenticationService service) : base(service)
    {
    }

    /// <summary>
    /// Login to the system using credentials.
    /// </summary>
    /// <param name="authenticateDto"></param>
    [Route("login")]
    [HttpPost]
    [ProducesResponseType(typeof(AuthenticateViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AuthenticatedStream([FromBody] AuthenticateDto authenticateDto)
        => Ok(
            await Service.AuthenticateAsync(authenticateDto)
        );

    [Route("token/refresh")]
    [HttpPost]
    [ProducesResponseType(typeof(AuthenticateViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDTO refreshTokenDto)
        => Ok(
            await Service.RefreshTokenAsync(refreshTokenDto.Token)
        );

    /// <summary>
    /// Revoke Token
    /// </summary>
    /// <param name="refreshTokenDto"></param>
    [Route("token/revoke")]
    [HttpPost]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RevokeToken([FromBody] RefreshTokenDTO refreshTokenDto)
    {
        await Service.RemoveTokenAsync(refreshTokenDto.Token);
        return Ok();
    }
}