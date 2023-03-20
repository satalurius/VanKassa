using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.Entities;
using VanKassa.Domain.Exceptions;

namespace VanKassa.Backend.Core.Services;

public class OutletService : IOutletService
{
    private readonly IDbContextFactory<VanKassaDbContext> _dbContextFactory;

    private readonly IMapper _mapper;

    public OutletService(IDbContextFactory<VanKassaDbContext> dbContextFactory, IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OutletDto>> GetOutletsAsync()
    {
        try
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();

            var outlets = await dbContext.Outlets.ToListAsync();

            return _mapper.Map<List<Outlet>, List<OutletDto>>(outlets);
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