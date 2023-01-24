using AutoMapper;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.ViewModels;

namespace VanKassa.Shared.Mappers;

public class DtoViewModelProfiles : Profile
{
    public DtoViewModelProfiles()
    {
        CreateMap<EmployeesDbDto, EmployeeViewModel>()
            .ForMember(mem => mem.Id,
                opt => opt.MapFrom(src => src.UserId))
            .ForMember(mem => mem.Addresses,
                opt => opt.MapFrom(src => src.Addresses))
            .ForMember(mem => mem.LastName,
                opt => opt.MapFrom(src => src.LastName))
            .ForMember(mem => mem.FirstName,
                opt => opt.MapFrom(src => src.FirstName))
            .ForMember(opt => opt.Patronymic,
                opt => opt.MapFrom(src => src.Patronymic))
            .ForMember(opt => opt.Role,
                opt => opt.MapFrom(src => src.RoleName))
            .ForMember(mem => mem.Photo,
                opt => opt.MapFrom(src => src.Photo));
        
        CreateMap<EditedEmployeeViewModel, EditedEmployeeDto>();

        CreateMap<EditedEmployeeDto, EditedEmployeeViewModel>()
            .ConvertUsing<EditedEmployeeDtoToViewModel>();
    }
}