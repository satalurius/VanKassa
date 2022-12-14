using VanKassa.Domain.Dtos;

namespace VanKassa.Backend.Core.Services.Interface;

public interface IEmployeeEditService
{
    Task SaveEmployeeAsync(EditedEmployeeDto editedEmployee);
    Task<EditedEmployeeDto> GetEditedEmployeeById(int employeeId);
    Task ChangeExistEmployeeAsync(EditedEmployeeDto changedEmployee);
}