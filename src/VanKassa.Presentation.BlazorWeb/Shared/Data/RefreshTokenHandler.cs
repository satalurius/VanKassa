using System.Net.Http.Headers;
using Blazored.LocalStorage;
using VanKassa.Presentation.BlazorWeb.Services;
using VanKassa.Presentation.BlazorWeb.Services.Interfaces;

namespace VanKassa.Presentation.BlazorWeb.Shared.Data
{
    public class RefreshTokenHandler : DelegatingHandler
    {
        private readonly RefreshTokenService refreshTokenService;
        private readonly ILocalStorageService localStorage;

        private readonly ITokenService tokenService;

        public RefreshTokenHandler(RefreshTokenService refreshTokenService,
            ILocalStorageService localStorage, ITokenService tokenService)
        {
            this.refreshTokenService = refreshTokenService;
            this.localStorage = localStorage;
            this.tokenService = tokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var absPath = request?.RequestUri?.AbsolutePath;

            if (absPath is null)
            {
                throw new InvalidOperationException();
            }

            if (absPath.Contains("token") && absPath.Contains("authentication"))
            {
                return await base.SendAsync(request, cancellationToken);

            }

            var token = await refreshTokenService.TryRefreshToken();

            if (string.IsNullOrEmpty(token))
            {
                var notExpiredToken = await tokenService.GetToken();
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", notExpiredToken?.JwtToken);

            }
            else
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}