using VanKassa.Domain.Entities;
using VanKassa.Domain.Enums;

namespace VanKassa.Backend.Core.Data.AdminDashboard.OrdersSort.ConcreteStrategies;
public class EmptySortConcreteStrategy : IOrdersSortStrategy
{
    public IQueryable<Order> SortOrders(IQueryable<Order> orders, SortDirection sortDirection)
        => sortDirection switch
        {
            SortDirection.Ascending => orders.OrderBy(ord => ord.Date),
            SortDirection.None => orders.OrderByDescending(ord => ord.Date),
            SortDirection.Descending => orders.OrderByDescending(ord => ord.Date),
        };
}
