namespace VanKassa.Domain.Entities;

/// <summary>
/// Модель базы данных, представляющая таблицу категорий.
/// </summary>
public class Category
{
    public required int CategoryId { get; set; }
    public required string Name { get; set; }
    
    public required IEnumerable<Product> Products { get; set; }

}