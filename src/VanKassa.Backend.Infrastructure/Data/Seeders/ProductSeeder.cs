using Microsoft.Extensions.Configuration;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Infrastructure.Data.Seeders;

public class ProductSeeder : DatabaseSeeder
{
    public ProductSeeder(VanKassaDbContext dbContext, IConfiguration configuration) : base(dbContext, configuration)
    {
    }

    public override void SeedIfEmpty()
    {
        if (DbContext.Products.Any())
        {
            return;
        }
        
        DbContext.Products.AddRange(ScaffoldProducts());
        DbContext.SaveChanges();
    }

    private IList<Product> ScaffoldProducts()
    {
        var categories = DbContext.Categories.ToList();

        var saltCategory = categories
            .FirstOrDefault(cat => cat.Name == BaseCategories.SaltCategory);

        var mainCategory = categories
            .FirstOrDefault(cat => cat.Name == BaseCategories.MainCategory);

        var dessertCategory = categories
            .FirstOrDefault(cat => cat.Name == BaseCategories.DesertCategory);

        if (saltCategory is null || mainCategory is null || dessertCategory is null)
        {
            return new List<Product>();
        }

        var saltProducts = new List<Product>
        {
            new()
            {
                Name = "Цезарь",
                Category = saltCategory,
                Price = 220,
            },

            new()
            {
                Name = "Вальдорф",
                Category = saltCategory,
                Price = 400
            }
        };

        var mainProducts = new List<Product>
        {
            new()
            {
                Name = "Окрошка с ветчиной",
                Price = 250,
                Category = mainCategory,
            },
            new()
            {
                Name = "Филе индейки с баклажанами и томатами",
                Price = 389,
                Category = mainCategory
            },
            new()
            {
                Name = "Карбонара",
                Price = 339,
                Category = mainCategory
            }
        };

        var dessertProducts = new List<Product>
        {
            new()
            {
                Name = "Тирамису",
                Price = 239,
                Category = dessertCategory
            },
            new()
            {
                Name = "Чизкейк",
                Price = 239,
                Category = dessertCategory
            }
        };

        var allProducts = saltProducts
            .Concat(mainProducts)
            .Concat(dessertProducts)
            .ToList();

        return allProducts;
    }
}