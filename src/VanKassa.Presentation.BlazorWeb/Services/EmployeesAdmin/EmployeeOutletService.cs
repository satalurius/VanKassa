using AutoMapper;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.Exceptions;
using VanKassa.Domain.ViewModels;
using VanKassa.Presentation.BlazorWeb.Services.Interfaces;
using VanKassa.Presentation.BlazorWeb.Shared.Data.Base;

namespace VanKassa.Presentation.BlazorWeb.Services.EmployeesAdmin;

public class EmployeeOutletService : ServiceBase
{
    public EmployeeOutletService(IHttpClientFactory httpClientFactory, IMapper mapper, IConfiguration config,
        ITokenService tokenService) : base(httpClientFactory, mapper, config, tokenService)
    {
        WebApiAddress += "/outlets";
    }
    
    public async Task<List<EmployeeOutletViewModel>> GetOutletsAsync()
    {
        var uri = WebApiAddress + "/all";

        var roles = await GetAsync<List<OutletDto>>(uri);

        if (roles is null)
        {
            throw new NotFoundException("Точки не найдены");
        }

        return Mapper.Map<List<OutletDto>, List<EmployeeOutletViewModel>>(roles);
    }
}