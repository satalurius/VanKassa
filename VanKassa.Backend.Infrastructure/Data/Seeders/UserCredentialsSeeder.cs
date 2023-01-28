using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace VanKassa.Backend.Infrastructure.Data.Seeders;

public class UserCredentialsSeeder : DatabaseSeeder
{
    public UserCredentialsSeeder(VanKassaDbContext dbContext, IConfiguration configuration) : base(dbContext, configuration)
    {
    }

    public override void SeedIfEmpty()
    {
        if (DbContext.UsersCredentials.Any())
            return;

        var sqlScript = File.ReadAllText(GetSqlScriptSetting()
            .ModelToUserCredentialsPathString());

        DbContext.Database.ExecuteSqlRaw(sqlScript);
    }
}