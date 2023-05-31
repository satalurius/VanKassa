using VanKassa.Domain.Entities;
using VanKassa.Domain.Enums;

namespace VanKassa.Backend.Core.Data.AdminDashboard.OrdersSort.ConcreteStrategies;
public class OutletSortConcreteStrategy : IOrdersSortStrategy
{
    public IQueryable<Order> SortOrders(IQueryable<Order> orders, SortDirection sortDirection)
         => sortDirection switch
         {
             SortDirection.None or SortDirection.Ascending => orders.OrderBy(ord => ord.OutletId),
             SortDirection.Descending => orders.OrderByDescending(ord => ord.OutletId),
         };
}
