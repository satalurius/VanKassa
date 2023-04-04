using VanKassa.Domain.Dtos.Admins;
using VanKassa.Domain.Dtos.Admins.Requests;

namespace VanKassa.Backend.Core.Services.Interface
{
    public interface IAdministratorsService
    {
        Task<IReadOnlyList<AdministratorDto>> GetAdministratorsAsync();
        Task CreateAdministratorAsync(CreateAdministratorRequest createAdministratorRequest);
        Task DeleteAdministratorsAsync(DeleteAdministratorsRequest deleteAdministratorRequest);
        Task ChangeAdministratorAsync(ChangeAdministratorRequest changeAdministratorRequest);
    }
}
