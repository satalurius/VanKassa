namespace VanKassa.Domain.Dtos.AdminDashboard.Orders;

public class PageOrderDto
{
    public int TotalCount { get; set; }
    public IList<OrderDto> Orders { get; set; } = new List<OrderDto>();
}