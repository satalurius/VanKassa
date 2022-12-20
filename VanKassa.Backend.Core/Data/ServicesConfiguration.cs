using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VanKassa.Backend.Core.AutoMappersConfig;
using VanKassa.Backend.Core.Data.EmployeesSort;
using VanKassa.Backend.Core.Services;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain;

namespace VanKassa.Backend.Core.Data;

public static class ServicesConfiguration
{

    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DbEntitiesToViewModelsMapper));
        services.AddScoped<IEmployeesService, EmployeesService>();
        services.AddSingleton<SortEmployeesExecutor>();

        services.AddSingleton<ImageService>();
        
        return services;
    }
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<VanKassaDbContext>(x => x.UseNpgsql(
            configuration.GetConnectionString(SettingConstant.PostgresDatabase),
            y => y.MigrationsAssembly(typeof(VanKassaDbContext).Assembly.FullName)));

        return services;
    }
}