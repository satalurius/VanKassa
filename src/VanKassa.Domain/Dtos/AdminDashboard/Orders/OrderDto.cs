using VanKassa.Domain.Dtos.AdminDashboard.Orders.Products;
using VanKassa.Domain.Entities;

namespace VanKassa.Domain.Dtos.AdminDashboard.Orders;

public class OrderDto
{
    public Guid OrderId { get; set; }
    public DateTime Date { get; set; }
    public bool Canceled { get; set; }
    public decimal Price { get; set; }
    public OutletDto? Outlet { get; set; }
    public IList<ProductDto> Products { get; set; } = new List<ProductDto>();
}