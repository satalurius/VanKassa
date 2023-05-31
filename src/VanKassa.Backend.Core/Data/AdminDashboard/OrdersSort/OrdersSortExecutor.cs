using VanKassa.Backend.Core.Data.AdminDashboard.OrdersSort.ConcreteStrategies;
using VanKassa.Domain.Enums.AdminDashboard.Orders;

namespace VanKassa.Backend.Core.Data.AdminDashboard.OrdersSort;
public class OrdersSortExecutor
{
    public IOrdersSortStrategy GetSortImplemetationByColumn(OrderTableColumn column)
        => column switch
        {
            OrderTableColumn.None => new EmptySortConcreteStrategy(),
            OrderTableColumn.Date => new DateSortConcreteStrategy(),
            OrderTableColumn.Status => new StatusSortConcreteStrategy(),
            OrderTableColumn.Price => new PriceSortConcreteStrategy(),
            OrderTableColumn.OutletName => new OutletSortConcreteStrategy(),
        };
}
