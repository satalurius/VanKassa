using Microsoft.AspNetCore.Components.Authorization;

namespace VanKassa.Presentation.BlazorWeb.Services
{
    public class RefreshTokenService
    {
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly AuthenticationService authenticationService;

        public RefreshTokenService(AuthenticationStateProvider authenticationStateProvider,
            AuthenticationService authenticationService)
        {
            this.authenticationStateProvider = authenticationStateProvider;
            this.authenticationService = authenticationService;
        }

        public async Task<string> TryRefreshToken()
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            var expValue = user.FindFirst(c => c.Type.Equals("exp"))?.Value;
            var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(expValue)).UtcDateTime;

            var timeUtc = DateTime.UtcNow.ToUniversalTime();

            var diff = expTime - timeUtc;

            if (timeUtc >= expTime || diff.TotalMilliseconds <= 10) 
            {
                return await authenticationService.RefreshToken();
            }

            return string.Empty;
        }
    }
}