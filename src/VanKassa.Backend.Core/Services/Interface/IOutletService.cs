using VanKassa.Domain.Dtos;
using VanKassa.Domain.Dtos.AdminDashboard.Outlets.Requests;

namespace VanKassa.Backend.Core.Services.Interface;

public interface IOutletService
{
    Task<IList<OutletDto>> GetOutletsAsync();
}