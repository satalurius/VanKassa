using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Requests;
using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Domain.Dtos.AdminDashboard.Outlets.Requests;
using VanKassa.Domain.Dtos.AdminDashboard.Statistics;

namespace VanKassa.Backend.Core.Services.Interface.AdminDashboard
{
    public interface IAdminDashboardStatisticService
    {
        Task<OrdersStatisticByPeriodDto> GetOrdersStatisticByPeriodAsync(GetOrdersByPeriodRequest request);
        Task<IList<SoldOrderByMonthDto>> GetOrdersStatisticByEveryMonth(GetOrdersByEveryMonthRequest request);
        Task<IList<RentalOutletDto>> StatisticForRentalOutletByPeriodAsync(GetRentalOutletRequestDto getRentalOutletRequest);
    }
}
