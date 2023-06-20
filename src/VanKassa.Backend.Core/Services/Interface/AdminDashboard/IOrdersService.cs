using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Requests;

namespace VanKassa.Backend.Core.Services.Interface.AdminDashboard;

public interface IOrdersService
{
    Task CreateOrderAsync(CreateOrderRequestDto request);
    Task<PageOrderDto> GetOrderByFilterAsync(OrdersPageParameters request);
}