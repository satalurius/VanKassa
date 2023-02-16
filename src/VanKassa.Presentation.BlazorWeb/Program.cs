using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using VanKassa.Presentation.BlazorWeb;
using VanKassa.Presentation.BlazorWeb.Services.EmployeesAdmin;
using VanKassa.Shared.Data;
using VanKassa.Shared.Mappers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredToast();

builder.Services.AddMudServices();
builder.Services.AddScoped<EmployeesService>();
builder.Services.AddScoped<EmployeeEditService>();
builder.Services.AddScoped<EmployeeRoleService>();
builder.Services.AddScoped<EmployeeOutletService>();
builder.Services.AddScoped<ImageConverter>();

builder.Services.AddAutoMapper(typeof(DtoViewModelProfiles));

await builder.Build().RunAsync();
