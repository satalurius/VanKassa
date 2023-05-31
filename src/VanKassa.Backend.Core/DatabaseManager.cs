using Microsoft.Extensions.Configuration;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Backend.Infrastructure.Data.Seeders;

namespace VanKassa.Backend.Core;

public class DatabaseManager
{
    private readonly VanKassaDbContext dbContext;
    private readonly IConfiguration configuration;

    public DatabaseManager(VanKassaDbContext dbContext, IConfiguration configuration)
    {
        this.dbContext = dbContext;
        this.configuration = configuration;
    }

    public void SeedDatabase()
    {
        SeedRoleTable();
        SeedUserTable();
        SeedOutletTable();
        SeedUserOutletTable();
        SeedCategory();
        SeedProduct();
    }

    private void SeedRoleTable()
    {
        var roleSeeder = new RoleSeeder(dbContext, configuration);
        
        roleSeeder.SeedIfEmpty();
    }

    private void SeedUserTable()
    {
        var userSeeder = new UserSeeder(dbContext, configuration);
        
        userSeeder.SeedIfEmpty();
    }

    private void SeedOutletTable()
    {
        var outletSeeder = new OutletSeeder(dbContext, configuration);
        
        outletSeeder.SeedIfEmpty();
    }

    private void SeedUserOutletTable()
    {
        var userOutletSeeder = new UserOutletSeeder(dbContext, configuration);
        
        userOutletSeeder.SeedIfEmpty();
    }

    private void SeedUserCredentialsTable()
    {
        var userCredentials = new UserCredentialsSeeder(dbContext, configuration);
        
        userCredentials.SeedIfEmpty();
    }

    private void SeedCategory()
    {
        var category = new CategorySeeder(dbContext, configuration);
        
        category.SeedIfEmpty();
    }

    private void SeedProduct()
    {
        var product = new ProductSeeder(dbContext, configuration);
        
        product.SeedIfEmpty();
    }
}