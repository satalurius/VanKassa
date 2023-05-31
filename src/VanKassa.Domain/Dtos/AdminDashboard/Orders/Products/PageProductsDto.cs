namespace VanKassa.Domain.Dtos.AdminDashboard.Orders.Products;

public class PageProductsDto
{
    public int TotalCount { get; set; }
    public IList<ProductDto> Products { get; set; } = new List<ProductDto>();
}