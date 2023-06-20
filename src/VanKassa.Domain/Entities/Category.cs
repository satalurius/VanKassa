namespace VanKassa.Domain.Entities;

/// <summary>
/// Модель базы данных, представляющая таблицу категорий.
/// </summary>
public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;

    public IEnumerable<Product> Products { get; set; } = new List<Product>();
}