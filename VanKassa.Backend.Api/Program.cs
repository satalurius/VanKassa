using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using VanKassa.Backend.Core.Data;
using VanKassa.Domain.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddNewtonsoftJson()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.AllowTrailingCommas = true;
        options.JsonSerializerOptions.WriteIndented = true;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
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


builder.Services
    .AddCors(options =>
    {
        options.AddPolicy(name: PolicyConstants.WebPolicy,
            policy =>
            {
                policy.WithOrigins("http://localhost:5000", "https://localhost:5001")
                    .WithHeaders(HeaderNames.ContentType)
                    .AllowAnyMethod();
            });
    });

builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();