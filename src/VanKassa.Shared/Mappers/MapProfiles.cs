using AutoMapper;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Categories;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Products;
using VanKassa.Domain.Dtos.AdminDashboard.Statistics.Outlets;
using VanKassa.Domain.Dtos.AdminDashboard.Statistics.TopProductStatistic;
using VanKassa.Domain.Dtos.Admins;
using VanKassa.Domain.Dtos.Admins.Requests;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Dtos.Employees.Requests;
using VanKassa.Domain.Entities;
using VanKassa.Domain.ViewModels;
using VanKassa.Domain.ViewModels.AdminDashboardViewModels;
using VanKassa.Domain.ViewModels.AdminDashboardViewModels.StatisticsViewModel;

namespace VanKassa.Shared.Mappers;

public class MapProfiles : Profile
{
    public MapProfiles()
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

        CreateMap<PageEmployeesDto, PageEmployeesViewModel>()
            .ForMember(mem => mem.TotalCount,
                opt => opt.MapFrom(src => src.TotalCount))
            .ForMember(mem => mem.EmployeesViewModels,
                opt => opt.MapFrom(src => src.EmployeesDbDtos));

        CreateMap<EditedEmployeeViewModel, EditedEmployeeDto>();

        CreateMap<EditedEmployeeDto, EditedEmployeeViewModel>()
            .ConvertUsing<EditedEmployeeDtoToViewModel>();

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

        CreateMap<EditedEmployeeViewModel, SavedEmployeeRequestDto>()
            .ConvertUsing<EditedEmployeeViewModelToSavedEmployeeRequestDto>();

        CreateMap<EditedEmployeeViewModel, ChangedEmployeeRequestDto>()
            .ConvertUsing<EditedEmployeeViewModelToChangedEmployeeRequestDto>();

        CreateMap<AuthorizationViewModel, AuthenticateDto>();

        CreateMap<GeneratePdfEmployeesRequest, PdfEmployeesViewModel>()
            .ForMember(mem => mem.EmployeeViewModels,
                opt => opt.MapFrom(src => src.EmployeesList))
            .ForMember(mem => mem.Outlet,
                opt => opt.MapFrom(src => src.Outlet));

        CreateMap<PdfEmployeeDto, PdfEmployeeViewModel>();
        CreateMap<PdfEmployeeViewModel, PdfEmployeeDto>();


        CreateMap<Administrator, AdministratorDto>()
            .ForMember(mem => mem.AdminId,
                opt => opt.MapFrom(src => src.UserId));

        CreateMap<AdministratorDto, Administrator>()
            .ForMember(mem => mem.UserId,
                opt => opt.MapFrom(src => src.AdminId));

        CreateMap<AdministratorViewModel, CreateAdministratorRequest>()
            .ConvertUsing<AdministratorViewModelToCreateAdministratorRequest>();

        CreateMap<AdministratorDto, AdministratorViewModel>()
            .ConvertUsing<AdministratorDtoToAdministratorViewModel>();

        CreateMap<AdministratorViewModel, ChangeAdministratorRequest>()
            .ConvertUsing<AdministratorViewModelToChangeAdministratorRequest>();


        #region AdminDashboardMapps

        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();
        
        CreateMap<Product, ProductDto>();
        CreateMap<ProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();


        CreateMap<Category, CategoryDto>();

        CreateMap<Order, OrderDto>()
            .ConvertUsing<OrderEntityToOrderDto>();

        CreateMap<TopProductDto, TopProductViewModel>();
        CreateMap<TopProductsDto, TopProductsViewModel>();

        CreateMap<RaitingOutletDto, RaitingOutletViewModel>();
        CreateMap<RaitingOutletViewModel, RaitingOutletDto>();

        CreateMap<PageOrderDto, TableOrderViewModel>();
        CreateMap<OrderDto, OrderViewModel>();

        CreateMap<OutletDto, OrderOutletViewModel>()
            .ConvertUsing<OutletDtoToOrderOutletViewModel>();

        CreateMap<ProductDto, ProductViewModel>();
        CreateMap<CategoryDto, CategoryViewModel>();

        #endregion
    }
}