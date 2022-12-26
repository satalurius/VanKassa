using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace VanKassa.Backend.Infrastructure.Data.Seeders;

public class UserSeeder : DatabaseSeeder
{
    public UserSeeder(VanKassaDbContext dbContext, IConfiguration configuration)
        : base(dbContext, configuration)
    {
    }

    public override void SeedIfEmpty()
    {
        if (DbContext.Users.Any())
            return;

        var sqlScript = File.ReadAllText(
            GetSqlScriptSetting()
                .ModelToUserPathString());
        
        DbContext.Database.ExecuteSqlRaw(sqlScript);
    }
}