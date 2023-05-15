using System.Text.Json.Serialization;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Products;
using VanKassa.Domain.Enums;
using VanKassa.Domain.Enums.AdminDashboard.Orders;

namespace VanKassa.Domain.Dtos.AdminDashboard.Orders;

public class CreateOrderRequestDto
{
    public DateTime Date { get; set; }
    public IList<int> ProductsIds { get; set; } = new List<int>();
    public decimal Price { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus OrderStatus { get; set; }
    public int OutletId { get; set; }
}