using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace VanKassa.Backend.Infrastructure.Data.Seeders;

public class UserOutletSeeder : DatabaseSeeder
{
    public UserOutletSeeder(VanKassaDbContext dbContext, IConfiguration configuration) : base(dbContext, configuration)
    {
    }

    public override void SeedIfEmpty()
    {
        if (DbContext.Outlets.Any())
            return;

        var sqlScript = File.ReadAllText(
            GetSqlScriptSetting()
                .ModelToOutletUserPathString());

        DbContext.Database.ExecuteSqlRaw(sqlScript);
    }
}