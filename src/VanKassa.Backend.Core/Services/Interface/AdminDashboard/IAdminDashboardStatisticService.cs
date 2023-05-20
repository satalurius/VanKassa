using VanKassa.Domain.Dtos.AdminDashboard.Orders.Requests;
using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Domain.Dtos.AdminDashboard.Statistics;
using VanKassa.Domain.Dtos.AdminDashboard.Statistics.Requests;
using VanKassa.Domain.Dtos.AdminDashboard.Statistics.TopProductStatistic;

namespace VanKassa.Backend.Core.Services.Interface.AdminDashboard
{
    public interface IAdminDashboardStatisticService
    {
        Task<OrdersStatisticByPeriodDto> GetOrdersStatisticByPeriodAsync(GetOrdersByPeriodRequest request);
        Task<IList<SoldOrderByMonthDto>> GetOrdersStatisticByEveryMonth(GetOrdersByEveryMonthRequest request);
        Task<IList<RentalOutletDto>> StatisticForRentalOutletByPeriodAsync(GetRentalOutletRequestDto request);
        Task<TopProductsDto> GetStatisticsForTopProductsByPriceAsync(GetTopProductsRequestDto request);
    }
}
