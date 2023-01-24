using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Dtos.Employees.Requests;

namespace VanKassa.Backend.Core.Services.Interface;

public interface IEmployeeEditService
{
    Task SaveEmployeeAsync(SavedEmployeeRequestDto savedEmployeeRequest);
    Task<EditedEmployeeDto> GetEditedEmployeeByIdAsync(int employeeId);
    Task ChangeEmployeeAsync(ChangedEmployeeRequestDto changedEmployeeRequest);
}