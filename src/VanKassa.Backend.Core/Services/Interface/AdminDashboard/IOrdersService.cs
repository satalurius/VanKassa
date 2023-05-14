using VanKassa.Domain.Dtos.AdminDashboard.Orders;

namespace VanKassa.Backend.Core.Services.Interface.AdminDashboard;

public interface IOrdersService
{
    Task CreateOrderAsync(CreateOrderRequestDto request);
}