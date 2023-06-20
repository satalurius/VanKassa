namespace VanKassa.Domain.Entities;

public class OrderProduct
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = new();

    public int ProductId { get; set; }
    public Product Product { get; set; } = new();
}