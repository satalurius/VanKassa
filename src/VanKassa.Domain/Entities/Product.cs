namespace VanKassa.Domain.Entities;

/// <summary>
/// Модель базы данных, представляющая таблицу категорий.
/// </summary>
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }

    /// <summary>
    /// Продукт по бизнес-логике имеет одну категорию.
    /// </summary>
    public virtual Category Category { get; set; } = new();

    public virtual IEnumerable<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}