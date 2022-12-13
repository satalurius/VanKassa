namespace VanKassa.Domain.Entities;

public class OrderProduct
{
    public required int OrderId { get; set; }
    public required Order Order { get; set; }
    
    public required int ProductId { get; set; }
    public required Product Product { get; set; }
}