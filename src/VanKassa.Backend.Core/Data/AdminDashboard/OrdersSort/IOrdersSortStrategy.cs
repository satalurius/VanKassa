using VanKassa.Domain.Entities;
using VanKassa.Domain.Enums;

namespace VanKassa.Backend.Core.Data.AdminDashboard.OrdersSort;
public interface IOrdersSortStrategy
{
    IQueryable<Order> SortOrders(IQueryable<Order> orders, SortDirection sortDirection);
}
