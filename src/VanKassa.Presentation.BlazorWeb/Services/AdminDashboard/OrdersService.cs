using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Domain.ViewModels.AdminDashboardViewModels;
using VanKassa.Presentation.BlazorWeb.Services.Interfaces;
using VanKassa.Presentation.BlazorWeb.Shared.Data.Base;

namespace VanKassa.Presentation.BlazorWeb.Services.AdminDashboard;

public class OrdersService : ServiceBase
{
    public OrdersService(IHttpClientFactory httpClientFactory, IMapper mapper, IConfiguration config, ITokenService tokenService) : base(httpClientFactory, mapper, config, tokenService)
    {
        WebApiAddress += "/orders";
    }

    public async Task<TableOrderViewModel?> GetShortOrdersAsync()
    {
        var query = new Dictionary<string, string>
        {
            ["Page"] = "0",
            ["PageSize"] = "5",
        };

        var uri = QueryHelpers.AddQueryString(WebApiAddress, query);

        try
        {
            var orders = await GetAsync<PageOrderDto>(uri);

            return Mapper.Map<TableOrderViewModel>(orders);
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }
}
