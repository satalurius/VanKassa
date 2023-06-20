using AutoMapper;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Exceptions;
using VanKassa.Domain.ViewModels;
using VanKassa.Presentation.BlazorWeb.Services.Interfaces;
using VanKassa.Presentation.BlazorWeb.Shared.Data.Base;

namespace VanKassa.Presentation.BlazorWeb.Services.EmployeesAdmin;

public class EmployeeRoleService : ServiceBase
{
    public EmployeeRoleService(IHttpClientFactory httpClientFactory, IMapper mapper,
        IConfiguration config, ITokenService tokenService) : base(httpClientFactory, mapper, config, tokenService)
    {
        WebApiAddress += "/roles";
    }
    
    public async Task<List<EmployeeRoleViewModel>> GetRolesAsync()
    {
        var roles = await GetAsync<List<EmployeesRoleDto>>(WebApiAddress);

        if (roles is null)
            throw new NotFoundException("Роли не найдены");

        return Mapper.Map<List<EmployeesRoleDto>, List<EmployeeRoleViewModel>>(roles);
    }
}