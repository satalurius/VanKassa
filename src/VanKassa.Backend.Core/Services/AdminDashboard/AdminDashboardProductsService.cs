using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanKassa.Backend.Core.Services.Interface.AdminDashboard;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Products;
using VanKassa.Domain.Entities;
using VanKassa.Domain.Exceptions;

namespace VanKassa.Backend.Core.Services.AdminDashboard;

public class AdminDashboardProductsService : IAdminDashboardProductsService
{
    private readonly IDbContextFactory<VanKassaDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public AdminDashboardProductsService(IMapper mapper, IDbContextFactory<VanKassaDbContext> dbContextFactory)
    {
        this.mapper = mapper;
        this.dbContextFactory = dbContextFactory;
    }

    public async Task CreateProductAsync(CreateProductDto createProduct)
    {
        try
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();

            var category = await dbContext.Categories
                .FirstOrDefaultAsync(category => category.CategoryId == createProduct.Category.CategoryId);

            if (category is null)
            {
                throw new NotFoundException("Category for product was not found");
            }

            var product = new Product
            {
                Name = createProduct.Name,
                Price = createProduct.Price,
                CategoryId = createProduct.Category.CategoryId,
                Category = category
            };

            await dbContext.Products.AddAsync(product);

            await dbContext.SaveChangesAsync();
        }
        catch (OperationCanceledException)
        {
            throw new BadRequestException("Произошла ошибка создания товара");
        }
        catch (ArgumentNullException)
        {
            throw new BadRequestException("Произошла ошибка создания товара");
        }
    }

    public async Task<PageProductsDto> GetProductsByFilterAsync(ProductsFilterDto productsFilter)
    {
        try
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();

            var productsQuery = dbContext.Products
                .Include(product => product.Category);

            var products = string.IsNullOrEmpty(productsFilter.CategoryName)
                ? await productsQuery
                    .ToListAsync()
                : await productsQuery
                    .Where(product => product.Category.Name.ToLower() == productsFilter.CategoryName.ToLower())
                    .ToListAsync();

            var productsDtos = mapper.Map<IList<ProductDto>>(products);

            return new PageProductsDto
            {
                TotalCount = products.Count,
                Products = productsDtos
            };
        }
        catch (OperationCanceledException)
        {
            throw new BadRequestException("Произошла ошибка получения товара");
        }
        catch (ArgumentNullException)
        {
            throw new BadRequestException("Произошла ошибка получения товара");
        }
    }

    public async Task UpdateProductAsync(UpdateProductDto updatedProduct)
    {
        try
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();

            var productEntity = await dbContext.Products
                .FirstAsync(product => product.ProductId == updatedProduct.ProductId);

            productEntity.Name = updatedProduct.Name;
            productEntity.Price = updatedProduct.Price;
            productEntity.CategoryId = updatedProduct.CategoryId;

            await dbContext.SaveChangesAsync();
        }
        catch (OperationCanceledException)
        {
            throw new BadRequestException("Произошла ошибка при обновлении товара");
        }
        catch (ArgumentNullException)
        {
            throw new BadRequestException("Произошла ошибка при обновлении товара");
        }
    }

    public async Task DeleteProductAsync(int deletedId)
    {
        try
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();

            var deletedProduct = await dbContext.Products
                .FirstOrDefaultAsync(product => product.ProductId == deletedId);

            if (deletedProduct is null)
            {
                throw new NotFoundException("Удаляемый товар не найден");
            }

            dbContext.Products.Remove(deletedProduct);
            await dbContext.SaveChangesAsync();
        }
        catch (OperationCanceledException)
        {
            throw new BadRequestException("Произошла ошибка при удалении товара");
        }
        catch (ArgumentNullException)
        {
            throw new BadRequestException("Произошла ошибка при удалении товара");
        }
    }
}