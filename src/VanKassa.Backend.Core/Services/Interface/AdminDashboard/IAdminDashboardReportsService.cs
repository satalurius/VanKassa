using ClosedXML.Excel;
using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Requests.OrdersReportRequest;

namespace VanKassa.Backend.Core.Services.Interface.AdminDashboard;
public interface IAdminDashboardReportsService
{
    Task<OrdersReportDto> GenerateOrdersReportAsync();
}
