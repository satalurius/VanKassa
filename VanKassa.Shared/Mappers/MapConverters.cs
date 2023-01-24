using AutoMapper;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.ViewModels;

namespace VanKassa.Shared.Mappers;

public class EditedEmployeeDtoToViewModel : ITypeConverter<EditedEmployeeDto, EditedEmployeeViewModel>
{
    public EditedEmployeeViewModel Convert(EditedEmployeeDto source, EditedEmployeeViewModel destination,
        ResolutionContext context)
        => new()
        {
            UserId = source.UserId,
            FirstName = source.FirstName,
            LastName = source.LastName,
            Patronymic = source.Patronymic,
            Outlets = source.Outlets.Select(outl => new EmployeeOutletViewModel
            {
                Id = outl.Id,
                Address = string.Join(", ", outl.City, outl.Street, outl.StreetNumber)
            }),
            Roles = source.Roles.Select(role => new EmployeeRoleViewModel
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName
            })
        };
}