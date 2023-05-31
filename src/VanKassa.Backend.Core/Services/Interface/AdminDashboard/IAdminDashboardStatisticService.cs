using VanKassa.Domain.Dtos.AdminDashboard.Orders.Requests;
using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Domain.Dtos.AdminDashboard.Statistics.Requests;
using VanKassa.Domain.Dtos.AdminDashboard.Statistics.TopProductStatistic;
using VanKassa.Domain.Dtos.AdminDashboard.Statistics.Outlets;

namespace VanKassa.Backend.Core.Services.Interface.AdminDashboard
{
    public interface IAdminDashboardStatisticService
    {
        Task<OrdersStatisticByPeriodDto> GetOrdersStatisticByPeriodAsync(GetOrdersByPeriodRequest request);
        Task<IList<SoldOrderByMonthDto>> GetOrdersStatisticByEveryMonth(GetOrdersByEveryMonthRequest request);
        Task<IList<RentalOutletDto>> StatisticForRentalOutletByPeriodAsync(GetRentalOutletRequestDto request);
        Task<IList<RaitingOutletDto>> StatisticsForRaitingsOutletsByPeriodAsync(GetRaitingOutletsRequestDto request);
        Task<TopProductsDto> GetStatisticsForTopProductsByPriceAsync(GetTopProductsRequestDto request);
        Task<MoneyForMonthDto> GetMoneyForMonthAsync(MoneyForMonthRequest request);
    }
}
