using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using VanKassa.Backend.Core.Data.AdminDashboard.OrdersSort;
using VanKassa.Backend.Core.Data.EmployeesSort;
using VanKassa.Backend.Core.Services;
using VanKassa.Backend.Core.Services.AdminDashboard;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Backend.Core.Services.Interface.AdminDashboard;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Backend.Infrastructure.IdentityEntities;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Models.SettingsModels;
using VanKassa.Shared.Mappers;

namespace VanKassa.Backend.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<VanKassaDbContext>(p => p.GetRequiredService<IDbContextFactory<VanKassaDbContext>>()
            .CreateDbContext());
        services.AddAutoMapper(typeof(MapProfiles));

        services.AddTransient<UserManager<LoginUser>>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddScoped<IEmployeesService, EmployeesService>();
        services.AddScoped<IEmployeesRoleService, EmployeesRoleService>();
        services.AddScoped<IOutletService, OutletService>();
        services.AddScoped<IEmployeeEditService, EmployeeEditService>();
        services.AddScoped<IEmployeesPdfService, EmployeesPdfService>();

        services.AddScoped<IAdministratorsService, AdministratorService>();
        
        services.AddSingleton<SortEmployeesExecutor>();

        services.AddSingleton<IImageService, ImageService>();

        services.AddScoped<IAdminDashboardProductsService, AdminDashboardProductsService>();
        services.AddScoped<IAdminDashboardCategoriesService, AdminDashboardCategoriesService>();
        services.AddScoped<IOrdersService, OrdersService>();
        services.AddScoped<IAdminDashboardStatisticService, AdminDashboardStatisticService>();

        services.AddScoped<OrdersSortExecutor>();
        return services;
    }

    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(SettingsConstants.PostgresDatabase);

        services.AddDbContextFactory<VanKassaDbContext>(x => x.UseNpgsql(
            configuration.GetConnectionString(SettingsConstants.PostgresDatabase),
            y => y.MigrationsAssembly(typeof(VanKassaDbContext).Assembly.FullName)));

        services.AddScoped<DapperDbContext>(x => new DapperDbContext(connectionString ?? throw new ArgumentNullException()));

        return services;
    }

    public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JWTSettings>(configuration.GetSection(nameof(JWTSettings)));
        services.Configure<DefaultSuperAdminSettings>(configuration.GetSection(nameof(DefaultSuperAdminSettings)));
        services.Configure<DefaultAdminSettings>(configuration.GetSection(nameof(DefaultAdminSettings)));
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = "VanKassa Application",
                Description = "API for cafe automation"
            });

            var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
        });
    }

    public static void AddIdentityAndAuthorization(this IServiceCollection services)
    {
        services.AddIdentityAndConfigure();

        services.ConfigureJwt();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
        });
    }

    private static void ConfigureJwt(this IServiceCollection services)
    {
        ServiceProvider serviceProvider = services.BuildServiceProvider();

        var jwtSettings = serviceProvider.GetService<IOptions<JWTSettings>>()?.Value;

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
#if DEBUG
                cfg.RequireHttpsMetadata = false;
#endif
                cfg.SaveToken = false;

                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings?.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.Default.GetBytes(jwtSettings?.Secret ?? string.Empty)),
                    RoleClaimType = CustomClaims.Roles,
                    ClockSkew = TimeSpan.Zero
                };
            });
    }

    private static void AddIdentityAndConfigure(this IServiceCollection services)
    {
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            options.Lockout.MaxFailedAccessAttempts = 6;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.RequireUniqueEmail = false;
        });

        services.AddIdentity<LoginUser, LoginRole>(options => options.User.RequireUniqueEmail = false)
            .AddEntityFrameworkStores<VanKassaDbContext>()
            .AddDefaultTokenProviders();
    }
}