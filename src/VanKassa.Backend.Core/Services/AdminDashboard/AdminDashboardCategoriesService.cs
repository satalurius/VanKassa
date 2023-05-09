using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanKassa.Backend.Core.Services.Interface.AdminDashboard;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Categories;
using VanKassa.Domain.Entities;
using VanKassa.Domain.Exceptions;

namespace VanKassa.Backend.Core.Services.AdminDashboard;

public class AdminDashboardCategoriesService : IAdminDashboardCategoriesService
{
    
    private readonly IDbContextFactory<VanKassaDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public AdminDashboardCategoriesService(IDbContextFactory<VanKassaDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task CreateCategoryAsync(CreateCategoryDto createCategory)
    {
        try
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();

            var possibleExistCategory = await dbContext.Categories
                .FirstOrDefaultAsync(category => category.Name.ToLower() == createCategory.Name.ToLower());

            if (possibleExistCategory is not null)
            {
                throw new BadRequestException("Категория существует");
            }

            var category = new Category
            {
                Name = createCategory.Name,
            };

            await dbContext.Categories.AddAsync(category);

            await dbContext.SaveChangesAsync();
        }
        catch (OperationCanceledException)
        {
            throw new BadRequestException("Произошла ошибка создания категории");
        }
        catch (ArgumentNullException)
        {
            throw new BadRequestException("Произошла ошибка создания категории");
        }
    }

    public async Task<PageCategoryDto> GetCategoriesAsync()
    {
        try
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();


            var categoriesEntities = await dbContext.Categories
                .ToListAsync();

            var categories = mapper.Map<IList<CategoryDto>>(categoriesEntities);

            return new PageCategoryDto
            {
                Categories = categories
            };
        }
        catch (OperationCanceledException)
        {
            throw new BadRequestException("Произошла ошибка получении категории");
        }
        catch (ArgumentNullException)
        {
            throw new BadRequestException("Произошла ошибка получении категории");
        }
    }

    public async Task UpdateCategoryAsync(CategoryDto updatedCategory)
    {
        try
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();

            var categoryEntity = await dbContext.Categories
                .FirstAsync(category => category.CategoryId == updatedCategory.CategoryId);

            categoryEntity.Name = updatedCategory.Name;

            await dbContext.SaveChangesAsync();
        }
        catch (OperationCanceledException)
        {
            throw new BadRequestException("Произошла ошибка при обновлении категории");
        }
        catch (ArgumentNullException)
        {
            throw new BadRequestException("Произошла ошибка при обновлении категории");
        }
    }

    public async Task DeleteCategoryAsync(int deletedId)
    {
        try
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();

            var deletedCategory = await dbContext.Categories
                .FirstOrDefaultAsync(category => category.CategoryId == deletedId);

            if (deletedCategory is null)
            {
                throw new NotFoundException("Удаляемая категория не найдена");
            }

            dbContext.Categories.Remove(deletedCategory);
            await dbContext.SaveChangesAsync();
        }
        catch (OperationCanceledException)
        {
            throw new BadRequestException("Произошла ошибка при удалении категории");
        }
        catch (ArgumentNullException)
        {
            throw new BadRequestException("Произошла ошибка при удалении категории");
        }
    }
}