using Microsoft.Extensions.Configuration;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Infrastructure.Data.Seeders;

public class CategorySeeder : DatabaseSeeder
{
    public CategorySeeder(VanKassaDbContext dbContext, IConfiguration configuration) : base(dbContext, configuration)
    {
    }

    public override void SeedIfEmpty()
    {
        if (DbContext.Categories.Any())
        {
            return;
        }

        DbContext.Categories.AddRange(ScaffoldCategories());
        DbContext.SaveChanges();
    }

    private IList<Category> ScaffoldCategories()
        => new List<Category>
        {
            new()
            {
                Name =  BaseCategories.SaltCategory,
            },
            new()
            {
                Name = BaseCategories.MainCategory
            },
            new()
            {
                Name = BaseCategories.DesertCategory
            }
        };
}