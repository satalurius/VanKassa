using System.Net.Http.Json;
using AutoMapper;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.Dtos.Employees;
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
        List<OutletDto>? roles = await HttpClient.GetFromJsonAsync<List<OutletDto>>(WebApiAddress);
        
        return Mapper.Map<List<OutletDto>, List<EmployeeOutletViewModel>>(roles);
    }
}