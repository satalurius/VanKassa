using System.Text.Json.Serialization;
using VanKassa.Domain.Enums;
using VanKassa.Domain.Enums.AdminDashboard.Orders;

namespace VanKassa.Domain.Dtos.AdminDashboard.Orders;

public class OrdersPageParameters
{
    public int Page { get; set; }
    public int PageSize { get; set; } = 5;
    public OrderTableColumn SortedColumn { get; set; }
    public SortDirection SortDirection { get; set; }
}