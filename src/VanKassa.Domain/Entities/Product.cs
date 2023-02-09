namespace VanKassa.Domain.Entities;

/// <summary>
/// Модель базы данных, представляющая таблицу категорий.
/// </summary>
public class Product
{
    public required int ProductId { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required int CategoryId { get; set; }
    /// <summary>
    /// Продукт по бизнес-логике имеет одну категорию.
    /// </summary>
    public required Category Category { get; set; }

    public required IEnumerable<OrderProduct> OrderProducts { get; set; }
}