using System.Net.Http.Json;
using AutoMapper;
using VanKassa.Presentation.BlazorWeb.Services.Interfaces;
using VanKassa.Presentation.BlazorWeb.Shared.Data.Constants;

namespace VanKassa.Presentation.BlazorWeb.Shared.Data.Base;

public abstract class ServiceBase
{
    protected readonly IMapper Mapper;

    protected string WebApiAddress;

    protected readonly ITokenService TokenService;

    private readonly IHttpClientFactory httpClientFactory;

    protected ServiceBase(IHttpClientFactory httpClientFactory, IMapper mapper, IConfiguration config, ITokenService tokenService)
    {
        this.Mapper = mapper;
        this.TokenService = tokenService;

        WebApiAddress = config.GetConnectionString("ApiAddress")
                        ?? throw new ArgumentNullException("Api address path does not exist");

        this.httpClientFactory = httpClientFactory;
    }

    protected async Task<TReturn?> GetAsync<TReturn>(string uri)
    {
        using var httpClient = httpClientFactory.CreateClient(HttpClientConstants.BackendApiClientConstant);

        return await httpClient.GetFromJsonAsync<TReturn>(uri);
    }

    protected async Task<HttpResponseMessage> PostAsync(string uri, StringContent httpContent)
    {
        using var httpClient = httpClientFactory.CreateClient(HttpClientConstants.BackendApiClientConstant);
        return await httpClient.PostAsync(uri, httpContent);
    }

    protected async Task<HttpResponseMessage> PostAsync<TContent>(string uri, TContent content)
    {
        using var httpClient = httpClientFactory.CreateClient(HttpClientConstants.BackendApiClientConstant);
        return await httpClient.PostAsJsonAsync(uri, content);
    }

    protected async Task<HttpResponseMessage> PatchAsync<TContent>(string uri, TContent content)
    {
        using var httpClient = httpClientFactory.CreateClient(HttpClientConstants.BackendApiClientConstant);
        return await httpClient.PatchAsJsonAsync(uri, content);
    }

    protected async Task<HttpResponseMessage> PutAsync<TContent>(string uri, TContent content)
    {
        using var httpClient = httpClientFactory.CreateClient(HttpClientConstants.BackendApiClientConstant);
        return await httpClient.PutAsJsonAsync(uri, content);
    }

    protected async Task<HttpResponseMessage> DeleteAsync(string uri)
    {
        using var httpClient = httpClientFactory.CreateClient(HttpClientConstants.BackendApiClientConstant);

        return await httpClient.DeleteAsync(uri);
    }

}