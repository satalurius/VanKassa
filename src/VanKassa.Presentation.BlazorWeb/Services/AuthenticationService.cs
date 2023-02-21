using System.Net.Http.Json;
using AutoMapper;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.Models;
using VanKassa.Domain.ViewModels;
using VanKassa.Presentation.BlazorWeb.Services.Interfaces;
using VanKassa.Presentation.BlazorWeb.Shared.Data;
using VanKassa.Presentation.BlazorWeb.Shared.Data.Base;

namespace VanKassa.Presentation.BlazorWeb.Services
{
    public class AuthenticationService : ServiceBase
    {
        private readonly ITokenService tokenService;
        private readonly JwtAuthenticationStateProvider jwtAuthenticationStateProvider;
        private readonly IHttpClientFactory httpClientFactory;

        public AuthenticationService(IHttpClientFactory httpClientFactory, IMapper mapper, IConfiguration config,
            ITokenService tokenService, JwtAuthenticationStateProvider jwtAuthenticationStateProvider) : base(httpClientFactory, mapper, config, tokenService)
        {
            WebApiAddress += "/authentication";
            this.tokenService = tokenService;
            this.jwtAuthenticationStateProvider = jwtAuthenticationStateProvider;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<bool> Authenticate(AuthorizationViewModel authorizationModel)
        {
            try
            {
                var uri = WebApiAddress + "/login";

                var authenticateDto = Mapper.Map<AuthenticateDto>(authorizationModel);

                var response = await PostAsync(uri, authenticateDto);

                var authenticationModel = await response.Content.ReadFromJsonAsync<AuthenticateViewModel>();

                if (authenticationModel is null ||
                    authenticationModel.JwtToken is null ||
                    authenticationModel.RefreshToken is null
                    )
                {
                    return false;
                }


                await tokenService.SetToken(
                    new Token(authenticationModel.JwtToken, authenticationModel.RefreshToken)
                );

                jwtAuthenticationStateProvider.StateChanged();

                return true;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public async Task<string> RefreshToken()
        {
            var token = await tokenService.GetToken();

            if (token is null)
            {
                return string.Empty;
            }

            var refreshTokenDto = new RefreshTokenDTO
            {
                Token = token.RefreshToken
            };

            var uri = $"{WebApiAddress}/token/refresh";

            var httpClient = httpClientFactory.CreateClient("BackendApi");

            var response = await httpClient.PostAsJsonAsync(uri, refreshTokenDto);

            var authenticationModel = await response.Content.ReadFromJsonAsync<AuthenticateViewModel>();

            if (authenticationModel is null)
            {
                throw new InvalidOperationException();
            }

            await tokenService.SetToken(
                new Token(authenticationModel.JwtToken, authenticationModel.RefreshToken)
            );

            jwtAuthenticationStateProvider.StateChanged();

            return authenticationModel.JwtToken;
        }
    }
}