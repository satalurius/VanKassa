using Microsoft.Extensions.Configuration;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Models.SettingsModels;

namespace VanKassa.Backend.Infrastructure.Data.Seeders;

public abstract class DatabaseSeeder
{
    protected readonly VanKassaDbContext DbContext;
    protected readonly IConfiguration Configuration;

    public DatabaseSeeder(VanKassaDbContext dbContext, IConfiguration configuration)
    {
        DbContext = dbContext;
        this.Configuration = configuration;
    }
    
    public abstract void SeedIfEmpty();

    public SqlScriptsSettings GetSqlScriptSetting()
    {
        var sqlScriptSetting = Configuration
            .GetSection(SettingsConstants.SqlScriptsSettingsName)
            .Get<SqlScriptsSettings>();

        if (sqlScriptSetting is null)
            throw new InvalidOperationException("Sql скрипт не найден");

        return sqlScriptSetting;
    }

}