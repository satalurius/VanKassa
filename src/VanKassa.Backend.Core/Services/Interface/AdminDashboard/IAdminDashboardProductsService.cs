using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Products;

namespace VanKassa.Backend.Core.Services.Interface.AdminDashboard;

public interface IAdminDashboardProductsService
{
    Task CreateProductAsync(CreateProductDto createProduct);
    Task<PageProductsDto> GetProductsByFilterAsync(ProductsFilterDto productsFilter);
    Task UpdateProductAsync(UpdateProductDto updatedProduct);
    Task DeleteProductAsync(int deletedId);
}