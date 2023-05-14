namespace VanKassa.Domain.Entities;

/// <summary>
/// Модель базы данных, представляющая таблицу заказов.
/// </summary>
public class Order
{
    public Guid OrderId { get; set; }
    public DateTime Date { get; set; }
    public bool Canceled { get; set; }
    public decimal Price { get; set; }

    public IEnumerable<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}