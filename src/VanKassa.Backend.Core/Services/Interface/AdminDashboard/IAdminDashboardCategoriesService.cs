using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Categories;

namespace VanKassa.Backend.Core.Services.Interface.AdminDashboard;

public interface IAdminDashboardCategoriesService
{
    Task CreateCategoryAsync(CreateCategoryDto createCategory);
    Task<PageCategoryDto> GetCategoriesAsync();
    Task UpdateCategoryAsync(CategoryDto updatedCategory);
    Task DeleteCategoryAsync(int deletedId);
}