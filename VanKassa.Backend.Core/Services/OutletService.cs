using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Core.Services;

public class OutletService : IOutletService
{
    private readonly VanKassaDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public OutletService(VanKassaDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OutletDto>> GetOutletsAsync()
    {
        var outlets = await _dbContext.Outlets.ToListAsync();

        return _mapper.Map<List<Outlet>, List<OutletDto>>(outlets);
    }
}