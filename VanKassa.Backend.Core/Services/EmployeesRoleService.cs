using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Core.Services;

public class EmployeesRoleService : IEmployeesRoleService
{
    private readonly IDbContextFactory<VanKassaDbContext> _dbContextFactory;
    private readonly IMapper _mapper;

    public EmployeesRoleService(IDbContextFactory<VanKassaDbContext> dbContextFactory, IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeesRoleDto>> GetAllRolesAsync()
    {
        try
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();

            var roles = await dbContext.Roles.ToListAsync();

            return _mapper.Map<List<Role>, List<EmployeesRoleDto>>(roles);
        }
        catch (ArgumentNullException)
        {
            throw new InvalidOperationException();
        }
        catch (InvalidOperationException)
        {
            throw new InvalidOperationException();
        }
    }
}