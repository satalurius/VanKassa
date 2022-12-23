using VanKassa.Domain.Dtos;

namespace VanKassa.Backend.Core.Services.Interface;

public interface IEmployeeEditService
{
    Task SaveEmployee(EditedEmployeeDto editedEmployee);
}