using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using VanKassa.Presentation.BlazorWeb;
using VanKassa.Presentation.BlazorWeb.Services;
using VanKassa.Presentation.BlazorWeb.Services.EmployeesAdmin;
using VanKassa.Presentation.BlazorWeb.Services.Interfaces;
using VanKassa.Presentation.BlazorWeb.Shared.Data;
using VanKassa.Shared.Data;
using VanKassa.Shared.Mappers;
using VanKassa.Presentation.BlazorWeb.Shared.Data.Constants;
using VanKassa.Presentation.BlazorWeb.Services.AdminServices;
using MudBlazor;
using VanKassa.Presentation.BlazorWeb.Services.AdminDashboard;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<EmployeesService>();
builder.Services.AddScoped<EmployeeEditService>();
builder.Services.AddScoped<EmployeeRoleService>();
builder.Services.AddScoped<EmployeeOutletService>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<EmployeesPdfReportService>();
builder.Services.AddScoped<AdministratorsService>();
builder.Services.AddScoped<CommonStatisticsService>();

builder.Services.AddScoped<ImageConverter>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<RefreshTokenService>();

builder.Services.AddScoped<RefreshTokenHandler>();

builder.Services.AddHttpClient(HttpClientConstants.BackendApiClientConstant,
    opt => opt.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
.AddHttpMessageHandler<RefreshTokenHandler>();


builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<JwtAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<JwtAuthenticationStateProvider>());

builder.Services.AddHttpClientInterceptor();

builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredLocalStorage();


builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});



builder.Services.AddAutoMapper(typeof(MapProfiles));

await builder.Build().RunAsync();

