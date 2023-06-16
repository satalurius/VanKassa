using VanKassa.Domain.Dtos.AdminDashboard.Orders.Products;

namespace VanKassa.Domain.Dtos.AdminDashboard.Orders.Requests.OrdersReportRequest;
public class GenerateOrdersData
{
    public Guid OrderId { get; set; }
    public DateTime Date { get; set; }
    public bool Canceled { get; set; }
    public decimal Price { get; set; }
    public string OutletName { get; set; } = string.Empty;

    public IList<ProductDto> Products { get; set; } = new List<ProductDto>();
}
