namespace VanKassa.Domain.Entities;

/// <summary>
/// Модель базы данных, представляющая таблицу заказов.
/// </summary>
public class Order
{
    public required int OrderId { get; set; }
    public required DateTime Date { get; set; }
    public required bool Canceled { get; set; }
    public required decimal Price { get; set; }
    
    public required IEnumerable<OrderProduct> OrderProducts { get; set; }
}