using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace VanKassa.Backend.Infrastructure.Data.Seeders;

public class RoleSeeder : DatabaseSeeder
{
    public RoleSeeder(VanKassaDbContext dbContext, IConfiguration configuration) : base(dbContext, configuration)
    {
    }

    public override void SeedIfEmpty()
    {
        if (DbContext.EmployeesRoles.Any())
            return;

        var sqlScript = File.ReadAllText(
            GetSqlScriptSetting()
                .ModelToRolePathString());

        DbContext.Database.ExecuteSqlRaw(sqlScript);
    }
}