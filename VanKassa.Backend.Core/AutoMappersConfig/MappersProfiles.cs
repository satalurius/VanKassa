using AutoMapper;
using VanKassa.Backend.Core.AutoMappersConfig.Converters;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Entities;
using VanKassa.Domain.ViewModels;

namespace VanKassa.Backend.Core.AutoMappersConfig;

public class MappersProfiles : Profile
{
    
    public MappersProfiles()
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

        CreateMap<Role, EmployeesRoleDto>()
            .ForMember(mem => mem.RoleId,
                opt => opt.MapFrom(src => src.RoleId))
            .ForMember(mem => mem.RoleName,
                opt => opt.MapFrom(src => src.Name));

        CreateMap<EmployeesRoleDto, EmployeeRoleViewModel>();

        CreateMap<Outlet, OutletDto>()
            .ForMember(mem => mem.Id,
                opt => opt.MapFrom(src => src.OutletId));

        CreateMap<OutletDto, EmployeeOutletViewModel>()
            .ConvertUsing<OutletDtoToOutletViewModelConverter>();
        
        CreateMap<EmployeeOutletViewModel, OutletDto>()
            .ConvertUsing<OutletViewModelToDtoConverter>();

        CreateMap<EmployeeRoleViewModel, RoleDto>();

        CreateMap<EditedEmployeeViewModel, EditedEmployeeDto>();

        CreateMap<EditedEmployeeDto, EditedEmployeeViewModel>()
            .ConvertUsing<EditedEmployeeDtoToViewModel>();

        CreateMap<User, EditedEmployeeDto>();
    }
}