using System.Net.Http.Json;
using AutoMapper;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.ViewModels;
using VanKassa.Presentation.BlazorWeb.Features.Shared.Data.Base;

namespace VanKassa.Presentation.BlazorWeb.Features.Admin.EmployeesEditor;

public class EmployeeRoleService : ServiceBase
{
    public EmployeeRoleService(HttpClient httpClient, IMapper mapper,
        IConfiguration config) : base(httpClient, mapper, config)
    {
        WebApiAddress += "/roles";
    }
    
    public async Task<List<EmployeeRoleViewModel>> GetRolesAsync()
    {
        var roles = await HttpClient.GetFromJsonAsync<List<EmployeesRoleDto>>(WebApiAddress);

        return Mapper.Map<List<EmployeesRoleDto>, List<EmployeeRoleViewModel>>(roles);
    }
}