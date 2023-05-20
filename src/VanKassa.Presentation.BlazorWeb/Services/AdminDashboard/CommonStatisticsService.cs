using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Domain.ViewModels.AdminDashboardViewModels.StatisticsViewModel;
using VanKassa.Presentation.BlazorWeb.Services.Interfaces;
using VanKassa.Presentation.BlazorWeb.Shared.Data.Base;
using VanKassa.Shared.Data.Helpers;

namespace VanKassa.Presentation.BlazorWeb.Services.AdminDashboard;

public class CommonStatisticsService : ServiceBase
{
    public CommonStatisticsService(IHttpClientFactory httpClientFactory, IMapper mapper, IConfiguration config, ITokenService tokenService) : base(httpClientFactory, mapper, config, tokenService)
    {
        WebApiAddress += "/statistics";
    }

    public async Task<CommonStatisticViewModel> GetCommonStatisticsAsync()
    {
        var moneyForMonthUri = WebApiAddress + "/money_for_month";
        var ordersByPeriodUri = WebApiAddress + "/orders/by_period";

        var date = DateTime.Now; 
        
        moneyForMonthUri = QueryHelpers.AddQueryString(moneyForMonthUri, "MonthDate", DateTimeHelper.ConvertDateTimeToShortDateQueryCorrectForm(date));


        var ordersByPeriodQuery = new Dictionary<string, string>
        {
            ["StartDate"] = DateTimeHelper.ConvertDateTimeToShortDateQueryCorrectForm(date),
            ["EndDate"] = DateTimeHelper.ConvertDateTimeToShortDateQueryCorrectForm(date)
        };

        ordersByPeriodUri = QueryHelpers.AddQueryString(ordersByPeriodUri, ordersByPeriodQuery);

        try
        {

            var moneysDto = await GetAsync<MoneyForMonthDto>(moneyForMonthUri);

            var ordersDto = await GetAsync<OrdersStatisticByPeriodDto>(ordersByPeriodUri);

            return new CommonStatisticViewModel(moneysDto?.Money ?? 0,
                ordersDto?.TotalMoney ?? 0,
                ordersDto?.Count ?? 0);
        }
        catch (HttpRequestException)
        {
            return new CommonStatisticViewModel(0, 0, 0);
        }

      
    }
}