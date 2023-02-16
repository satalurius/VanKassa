using AutoMapper;

namespace VanKassa.Presentation.BlazorWeb.Shared.Data.Base;

public abstract class ServiceBase
{
    protected readonly HttpClient HttpClient;
    protected readonly IMapper Mapper;
    
    protected string WebApiAddress;

    protected ServiceBase(HttpClient httpClient, IMapper mapper, IConfiguration config)
    {
        this.HttpClient = httpClient;
        this.Mapper = mapper;
        
        WebApiAddress = config.GetConnectionString("ApiAddress") 
                        ?? throw new ArgumentNullException("Api address path does not exist");
    }
}