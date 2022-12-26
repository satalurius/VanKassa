using VanKassa.Domain.Dtos;

namespace VanKassa.Backend.Core.Services.Interface;

public interface IOutletService
{
    Task<IEnumerable<OutletDto>> GetOutletsAsync();
}