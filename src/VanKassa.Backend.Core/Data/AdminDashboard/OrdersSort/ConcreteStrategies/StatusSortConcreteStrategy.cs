using VanKassa.Domain.Entities;
using VanKassa.Domain.Enums;

namespace VanKassa.Backend.Core.Data.AdminDashboard.OrdersSort.ConcreteStrategies;
public class StatusSortConcreteStrategy : IOrdersSortStrategy
{
    public IQueryable<Order> SortOrders(IQueryable<Order> orders, SortDirection sortDirection)
        => sortDirection switch
        {
            SortDirection.None or SortDirection.Ascending => orders.OrderBy(ord => ord.Canceled),
            SortDirection.Descending => orders.OrderByDescending(ord => ord.Canceled)
        };
}
