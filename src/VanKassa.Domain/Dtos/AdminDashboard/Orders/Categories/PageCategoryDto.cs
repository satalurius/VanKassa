namespace VanKassa.Domain.Dtos.AdminDashboard.Orders.Categories;

public class PageCategoryDto
{
    public IList<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
}