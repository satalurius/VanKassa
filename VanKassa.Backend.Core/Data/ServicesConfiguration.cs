using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VanKassa.Backend.Core.AutoMappersConfig;
using VanKassa.Backend.Core.Data.EmployeesSort;
using VanKassa.Backend.Core.Services;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Constants;

namespace VanKassa.Backend.Core.Data;

public static class ServicesConfiguration
{

    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<VanKassaDbContext>(p => p.GetRequiredService<IDbContextFactory<VanKassaDbContext>>()
            .CreateDbContext());
        services.AddAutoMapper(typeof(MappersProfiles));
        services.AddScoped<IEmployeesService, EmployeesService>();
        services.AddScoped<IEmployeesRoleService, EmployeesRoleService>();
        services.AddScoped<IOutletService, OutletService>();
        services.AddScoped<IEmployeeEditService, EmployeeEditService>();
        
        services.AddSingleton<SortEmployeesExecutor>();

        services.AddSingleton<ImageService>();

        services.AddScoped<CookieIdentity>();
        
        return services;
    }
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<VanKassaDbContext>(x => x.UseNpgsql(
            configuration.GetConnectionString(SettingsConstants.PostgresDatabase),
            y => y.MigrationsAssembly(typeof(VanKassaDbContext).Assembly.FullName)));

        return services;
    }
}