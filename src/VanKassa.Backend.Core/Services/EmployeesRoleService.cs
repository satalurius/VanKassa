using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Entities;
using VanKassa.Domain.Exceptions;

namespace VanKassa.Backend.Core.Services;

public class EmployeesRoleService : IEmployeesRoleService
{
    private readonly IDbContextFactory<VanKassaDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public EmployeesRoleService(IDbContextFactory<VanKassaDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<EmployeesRoleDto>> GetAllRolesAsync()
    {
        try
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();

            var roles = await dbContext.EmployeesRoles.ToListAsync();

            return mapper.Map<List<Role>, List<EmployeesRoleDto>>(roles);
        }
        catch (ArgumentNullException)
        {
            throw new NotFoundException();
        }
        catch (InvalidOperationException)
        {
            throw new NotFoundException();
        }
    }
}