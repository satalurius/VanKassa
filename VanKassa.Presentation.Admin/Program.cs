using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using MudBlazor.Services;
using VanKassa.Backend.Core.Data;
using VanKassa.Domain;
using VanKassa.Presentation.Admin.Extensions;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureServices();

builder.Services.AddMudServices();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build().SeedData();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();