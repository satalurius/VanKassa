using System.Net.Http.Json;
using AutoMapper;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.Exceptions;
using VanKassa.Domain.ViewModels;
using VanKassa.Presentation.BlazorWeb.Features.Shared.Data.Base;

namespace VanKassa.Presentation.BlazorWeb.Features.Admin.EmployeesEditor;

public class EmployeeOutletService : ServiceBase
{
    public EmployeeOutletService(HttpClient httpClient, IMapper mapper, IConfiguration config) : base(httpClient, mapper, config)
    {
        WebApiAddress += "/outlets";
    }
    
    public async Task<List<EmployeeOutletViewModel>> GetOutletsAsync()
    {
        var roles = await HttpClient.GetFromJsonAsync<List<OutletDto>>(WebApiAddress + "/all");

        if (roles is null)
            throw new NotFoundException("Точки не найдены");
        
        return Mapper.Map<List<OutletDto>, List<EmployeeOutletViewModel>>(roles);
    }
}