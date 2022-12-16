using VanKassa.Backend.Core;
using VanKassa.Backend.Infrastructure.Data;

namespace VanKassa.Presentation.Admin.Extensions;

public static class WebHostExtensions
{
    public static WebApplication SeedData(this WebApplication host)
    {
        using var scope = host.Services.CreateScope();

        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<VanKassaDbContext>();
            var configuration = services.GetRequiredService<IConfiguration>();

            var seeder = new DatabaseManager(context, configuration);
            seeder.SeedDatabase();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Произошла ошибка при заполнении базы данных");
        }

        return host;
    }
}