using VanKassa.Presentation.Admin.Data.StateContainers;

namespace VanKassa.Presentation.Admin.Extensions;

public static class ConfigureWebServicesExtensions
{
    public static IServiceCollection ConfigureStateServices(this IServiceCollection services)
    {
        services.AddScoped<UserInformationStateContainer>();

        return services;
    }
}