using VanKassa.Domain.Models;

namespace VanKassa.Presentation.BlazorWeb.Services.Interfaces
{
    public interface ITokenService
    {
        Task<Token?> GetToken();
        Task RemoveToken();
        Task SetToken(Token token);
    }
}