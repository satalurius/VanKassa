namespace VanKassa.Domain.ViewModels.AdminDashboardViewModels;
public class ProductViewModel
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public CategoryViewModel Category { get; set; } = new();
}
