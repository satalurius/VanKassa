using VanKassa.Domain.Dtos;
using VanKassa.Domain.ViewModels;

namespace VanKassa.Backend.Core.Services.Interface;

public interface IAuthenticationService
{
    Task<AuthenticateViewModel> AuthenticateAsync(AuthenticateDto authenticationDto);
    Task<AuthenticateViewModel> RefreshTokenAsync(string token);
    Task RemoveTokenAsync(string token);
}