namespace VanKassa.Domain.Dtos.AdminDashboard.Orders.Products;

public class CreateProductDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public CategoryDto Category { get; set; } = new();
}