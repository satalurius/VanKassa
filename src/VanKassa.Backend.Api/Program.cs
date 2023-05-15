using System.Text.Json.Serialization;
using VanKassa.Backend.Api.Extensions;
using VanKassa.Backend.Api.Middlewares;
using VanKassa.Domain.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
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
 options.UseInlineDefinitionsForEnums();   
});

builder.Services.AddRazorTemplating();

builder.Services
    .AddCors(options =>
    {
        options.AddPolicy(name: PolicyConstants.WebPolicy,
            policy => policy
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowAnyHeader());
    });


builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureServices();

builder.Services.AddSettings(builder.Configuration);
builder.Services.AddIdentityAndAuthorization();


var app = builder.Build()
    .CreateDatabase()
    .SeedData()
    .SeedIdentity(builder.Configuration);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();


app.UseMiddleware<ExceptionMiddleware>();

//app.UseHttpsRedirection();


app.UseCors(PolicyConstants.WebPolicy);

app.MapControllers();

app.Run();