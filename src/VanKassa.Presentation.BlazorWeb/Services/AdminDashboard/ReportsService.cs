using AutoMapper;
using VanKassa.Domain.Exceptions;
using VanKassa.Presentation.BlazorWeb.Services.Interfaces;
using VanKassa.Presentation.BlazorWeb.Shared.Data.Base;

namespace VanKassa.Presentation.BlazorWeb.Services.AdminDashboard;

public class ReportsService : ServiceBase
{
    public ReportsService(IHttpClientFactory httpClientFactory, IMapper mapper, IConfiguration config, ITokenService tokenService) : base(httpClientFactory, mapper, config, tokenService)
    {
        WebApiAddress += "/reports";
    }

    public async Task<Stream> GenerateOrdersReportAsync()
    {
        var uri = WebApiAddress + "/orders";

        try
        {
            var response = await PostAsync(uri, "");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }

            throw new NotFoundException();
        }
        catch (HttpRequestException)
        {
            throw new NotFoundException();
        }
    }
}
