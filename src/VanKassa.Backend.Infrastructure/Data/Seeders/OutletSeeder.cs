using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace VanKassa.Backend.Infrastructure.Data.Seeders;

public class OutletSeeder : DatabaseSeeder
{
    public OutletSeeder(VanKassaDbContext dbContext, IConfiguration configuration) : base(dbContext, configuration)
    {
    }

    public override void SeedIfEmpty()
    {
        if (DbContext.Outlets.Any())
            return;

        var sqlScript = File.ReadAllText(
            GetSqlScriptSetting()
                .ModelToOutletPathString());
        
        DbContext.Database.ExecuteSqlRaw(sqlScript);
    }
}