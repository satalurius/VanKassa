using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Domain.Dtos.AdminDashboard.Statistics.Outlets;
using VanKassa.Domain.Dtos.AdminDashboard.Statistics.Requests;
using VanKassa.Domain.Dtos.AdminDashboard.Statistics.TopProductStatistic;
using VanKassa.Domain.ViewModels.AdminDashboardViewModels.StatisticsViewModel;
using VanKassa.Presentation.BlazorWeb.Services.Interfaces;
using VanKassa.Presentation.BlazorWeb.Shared.Data.Base;
using VanKassa.Shared.Data;
using VanKassa.Shared.Data.Helpers;

namespace VanKassa.Presentation.BlazorWeb.Services.AdminDashboard;

public class StatisticsService : ServiceBase
{
    public StatisticsService(IHttpClientFactory httpClientFactory, IMapper mapper, IConfiguration config, ITokenService tokenService) : base(httpClientFactory, mapper, config, tokenService)
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

    public async Task<IList<SoldOrderByMonthViewModel>> GetMoneysForEveryMonthAsync(int yearDate)
    {
        var uri = WebApiAddress + "/orders/every_month";

        uri = QueryHelpers.AddQueryString(uri, "YearDate", yearDate.ToString());

        try
        {
            var soldOrdersDto = await GetAsync<List<SoldOrderByMonthDto>>(uri);

            if (soldOrdersDto is null)
            {
                return new List<SoldOrderByMonthViewModel>();
            }

            return soldOrdersDto
                 .Select(order => new SoldOrderByMonthViewModel
                 {
                     Month = EnumConverters.ConvertMonthEnumToRussianMonth(order.Month),
                     Count = order.Count,
                     TotalMoney = order.TotalMoney,
                     Year = order.Year
                 }
                 )
                 .ToList();
        }
        catch (HttpRequestException)
        {
            return new List<SoldOrderByMonthViewModel>();
        }
    }

    public async Task<TopProductsViewModel> GetTopProductsForThisMonthAsync(int positions)
    {
        var uri = WebApiAddress + "/products/top";

        var date = DateTime.Now;

        var firstDateOfMonth = new DateTime(date.Year, date.Month, 1);
        var lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);

        var query = new Dictionary<string, string>
        {
            ["StartDate"] = DateTimeHelper.ConvertDateTimeToShortDateQueryCorrectForm(firstDateOfMonth),
            ["EndDate"] = DateTimeHelper.ConvertDateTimeToShortDateQueryCorrectForm(lastDateOfMonth),
            ["Positions"] = positions.ToString()
        };

        uri = QueryHelpers.AddQueryString(uri, query);

        try
        {
            var topProducts = await GetAsync<TopProductsDto>(uri);

            if (topProducts is null)
            {
                return new TopProductsViewModel();
            }


            return Mapper.Map<TopProductsViewModel>(topProducts);
        }
        catch (HttpRequestException)
        {
            return new TopProductsViewModel();
        }
    }

    public async Task<IList<RaitingOutletViewModel>> GetRaitingsOutletsAsync(GetRaitingOutletsRequestDto request)
    {
        var uri = WebApiAddress + "/outlets/raiting";

        var query = new Dictionary<string, string>
        {
            ["StartDate"] = DateTimeHelper.ConvertDateTimeToShortDateQueryCorrectForm(request.StartDate),
            ["EndDate"] = DateTimeHelper.ConvertDateTimeToShortDateQueryCorrectForm(request.EndDate),
            ["Positions"] = request.Positions.ToString()
        };

        uri = QueryHelpers.AddQueryString(uri, query);

        try
        {
            var raitings = await GetAsync<List<RaitingOutletDto>>(uri);

            return Mapper.Map<List<RaitingOutletViewModel>>(raitings);
        }
        catch (HttpRequestException)
        {
            return new List<RaitingOutletViewModel>();
        }
    }
}