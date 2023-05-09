using VanKassa.Domain.Dtos.AdminDashboard.Orders.Categories;

namespace VanKassa.Domain.Dtos.AdminDashboard.Orders.Products;

public class ProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public CategoryDto Category { get; set; } = new();
}