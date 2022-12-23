using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Core.Services;

public class EmployeesRoleService : IEmployeesRoleService
{
    private readonly VanKassaDbContext _dbContext;
    private readonly IMapper _mapper;

    public EmployeesRoleService(VanKassaDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeesRoleDto>> GetAllRolesAsync()
    {
        var roles = await _dbContext.Roles.ToListAsync();

        return _mapper.Map<List<Role>, List<EmployeesRoleDto>>(roles);
    }
}