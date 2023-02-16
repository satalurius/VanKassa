using System.Net.Http.Json;
using AutoMapper;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Exceptions;
using VanKassa.Domain.ViewModels;
using VanKassa.Presentation.BlazorWeb.Shared.Data.Base;

namespace VanKassa.Presentation.BlazorWeb.Services.EmployeesAdmin;

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

        if (roles is null)
            throw new NotFoundException("Роли не найдены");

        return Mapper.Map<List<EmployeesRoleDto>, List<EmployeeRoleViewModel>>(roles);
    }
}