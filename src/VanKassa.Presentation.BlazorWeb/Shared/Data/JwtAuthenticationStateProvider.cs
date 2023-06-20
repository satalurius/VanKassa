using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using VanKassa.Presentation.BlazorWeb.Services.Interfaces;

namespace VanKassa.Presentation.BlazorWeb.Shared.Data
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenService tokenService;

        public JwtAuthenticationStateProvider(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        public void StateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await tokenService.GetToken();
            var identity = token is null || string.IsNullOrEmpty(token?.JwtToken)
                ? new ClaimsIdentity()
                : new ClaimsIdentity(ParseClaimsFromJwt(token.JwtToken), "jwt");
            
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt) 
        {
            var payload = jwt.Split(".")[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var jwtClaimsPairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (jwtClaimsPairs is null)
                throw new ArgumentNullException();

            return jwtClaimsPairs.Select(jcp => new Claim(jcp.Key, jcp.Value?.ToString() ?? string.Empty));
        } 

        private byte[] ParseBase64WithoutPadding(string base64) 
        {
            switch (base64.Length % 4) 
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}