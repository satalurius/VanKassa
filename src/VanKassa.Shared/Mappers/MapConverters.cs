using AutoMapper;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Dtos.Employees.Requests;
using VanKassa.Domain.ViewModels;

namespace VanKassa.Shared.Mappers;

public class OutletDtoToOutletViewModelConverter : ITypeConverter<OutletDto, EmployeeOutletViewModel>
{
    public EmployeeOutletViewModel Convert(OutletDto source, EmployeeOutletViewModel destination,
        ResolutionContext context)
        => new()
        {
            Id = source.Id,
            Address = string.Join(", ", source.City, source.Street, source.StreetNumber)
        };
}

public class OutletViewModelToDtoConverter : ITypeConverter<EmployeeOutletViewModel, OutletDto>
{
    public OutletDto Convert(EmployeeOutletViewModel source, OutletDto destination, ResolutionContext context)
    {
        var addressTypes = source.Address.Split(", ").ToList();

        return new OutletDto
        {
            Id = source.Id,
            City = addressTypes[0],
            Street = addressTypes[1],
            StreetNumber = addressTypes[2]
        };
    }
}

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
            Photo = source.Photo,
            Outlets = source.Outlets.Select(outl => new EmployeeOutletViewModel
            {
                Id = outl.Id,
                Address = string.Join(", ", outl.City, outl.Street, outl.StreetNumber)
            }),
            Role = new EmployeeRoleViewModel
            {
                RoleId = source.Role.RoleId,
                RoleName = source.Role.RoleName
            }
        };
}

public class
    EditedEmployeeViewModelToSavedEmployeeRequestDto : ITypeConverter<EditedEmployeeViewModel, SavedEmployeeRequestDto>
{
    public SavedEmployeeRequestDto Convert(EditedEmployeeViewModel source, SavedEmployeeRequestDto destination,
        ResolutionContext context)
        => new()
        {
            LastName = source.LastName,
            FirstName = source.FirstName,
            Patronymic = source.Patronymic,
            Photo = source.Photo,
            RoleId = source.Role.RoleId,
            OutletsIds = source.Outlets.Select(outlet => outlet.Id)
        };
}

public class EditedEmployeeViewModelToChangedEmployeeRequestDto : ITypeConverter<EditedEmployeeViewModel,
    ChangedEmployeeRequestDto>
{
    public ChangedEmployeeRequestDto Convert(EditedEmployeeViewModel source, ChangedEmployeeRequestDto destination,
        ResolutionContext context)
        => new()
        {
            UserId = source.UserId,
            LastName = source.LastName,
            FirstName = source.FirstName,
            Patronymic = source.Patronymic,
            Photo = source.Photo,
            RoleId = source.Role.RoleId,
            OutletsIds = source.Outlets.Select(outlet => outlet.Id)
        };
}