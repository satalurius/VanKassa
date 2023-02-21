using Blazored.LocalStorage;
using VanKassa.Domain.Models;
using VanKassa.Presentation.BlazorWeb.Services.Interfaces;

namespace VanKassa.Presentation.BlazorWeb.Services
{
    public class TokenService : ITokenService
    {
        private readonly ILocalStorageService localStorageService;

        public TokenService(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public async Task<Token?> GetToken()
        {
            return await localStorageService.GetItemAsync<Token>("token");
        }

        public async Task RemoveToken()
        {
            await localStorageService.RemoveItemAsync("token");
        }

        public async Task SetToken(Token token)
        {
            await RemoveToken();
            await localStorageService.SetItemAsync("token", token);
        }
    }
}