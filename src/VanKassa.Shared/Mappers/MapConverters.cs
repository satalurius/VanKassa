using AutoMapper;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Categories;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Products;
using VanKassa.Domain.Dtos.Admins;
using VanKassa.Domain.Dtos.Admins.Requests;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Dtos.Employees.Requests;
using VanKassa.Domain.Entities;
using VanKassa.Domain.ViewModels;
using VanKassa.Domain.ViewModels.AdminDashboardViewModels;
using VanKassa.Shared.Data.Helpers;

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


public class AdministratorViewModelToCreateAdministratorRequest : ITypeConverter<AdministratorViewModel, CreateAdministratorRequest>
{
    public CreateAdministratorRequest Convert(AdministratorViewModel source, CreateAdministratorRequest destination, ResolutionContext context)
    {
        var splitName = source.FullName.Split(" ");

        return new CreateAdministratorRequest
        {
            LastName = splitName[0],
            FirstName = splitName[1],
            Patronymic = splitName[2],
            Password = source.Password,
            Phone = source.Phone
        };
    }
}


public class AdministratorDtoToAdministratorViewModel : ITypeConverter<AdministratorDto, AdministratorViewModel>
{
    public AdministratorViewModel Convert(AdministratorDto source, AdministratorViewModel destination, ResolutionContext context)
        => new()
        {
            AdminId = source.AdminId,
            FullName = string.Join(" ", source.LastName, source.FirstName, source.Patronymic),
            UserName = source.UserName,
            Phone = source.Phone
        };
}

public class AdministratorViewModelToChangeAdministratorRequest : ITypeConverter<AdministratorViewModel, ChangeAdministratorRequest>
{
    public ChangeAdministratorRequest Convert(AdministratorViewModel source, ChangeAdministratorRequest destination, ResolutionContext context)
    {
        var splitName = source.FullName.Split(" ");

        return new ChangeAdministratorRequest
        {
            AdminId = source.AdminId,
            NewPassword = source.Password,
            LastName = splitName[0],
            FirstName = splitName[1],
            Patronymic = splitName[2],
            Phone = source.Phone
        };
    }
}

public class AdministratroViewModelToDeleteAdministratorRequest : ITypeConverter<AdministratorViewModel, DeleteAdministratorsRequest>
{
    public DeleteAdministratorsRequest Convert(AdministratorViewModel source, DeleteAdministratorsRequest destination, ResolutionContext context)
        => new()
        {
            DeletedIds = new int[] { source.AdminId }
        };
}

public class OrderEntityToOrderDto : ITypeConverter<Order, OrderDto>
{
    public OrderDto Convert(Order source, OrderDto destination, ResolutionContext context)
        => new()
        {
            Canceled = source.Canceled,
            Date = source.Date,
            OrderId = source.OrderId,
            Outlet = new OutletDto
            {
                Id = source.Outlet.OutletId,
                City = source.Outlet.City,
                Street = source.Outlet.Street,
                StreetNumber = source.Outlet.StreetNumber ?? string.Empty
            },
            Products = source.OrderProducts.Select(orderProduct => new ProductDto
            {
                ProductId = orderProduct.Product.ProductId,
                Name = orderProduct.Product.Name,
                Price = orderProduct.Product.Price,
                Category = new CategoryDto
                {
                    CategoryId = orderProduct.Product.CategoryId,
                    Name = orderProduct.Product.Category.Name
                }
            }).ToList(),
            Price = source.Price
        };
}

public class PageOrderDtoToTableOrderViewModel : ITypeConverter<PageOrderDto, TableOrderViewModel>
{
    public TableOrderViewModel Convert(PageOrderDto source, TableOrderViewModel destination, ResolutionContext context)
        => new()
        {
            TotalCount = source.TotalCount,
            Orders = source.Orders
                .Select(order => new OrderViewModel
                {
                    OrderId = order.OrderId,
                    Canceled = order.Canceled,
                    Date = order.Date,
                    Outlet = new OrderOutletViewModel
                    {
                        Id = order.Outlet.Id,
                        Address = OutletHelper.BuildOutletNameByAddresses(order.Outlet.City, order.Outlet.Street, order.Outlet.StreetNumber ?? string.Empty)
                    },
                    Price = order.Price,
                    Products = order.Products
                        .Select(product => new ProductViewModel
                        {
                            ProductId = product.ProductId,
                            Price = product.Price,
                            Category = new CategoryViewModel
                            {
                                CategoryId = product.Category.CategoryId,
                                Name = product.Category.Name
                            },
                            Name = product.Name
                        })
                        .ToList()
                })
                .ToList()
        };
}

public class OutletDtoToOrderOutletViewModel : ITypeConverter<OutletDto, OrderOutletViewModel>
{
    public OrderOutletViewModel Convert(OutletDto source, OrderOutletViewModel destination, ResolutionContext context)
    => new()
    {
        Id = source.Id,
        Address = OutletHelper.BuildOutletNameByAddresses(source.City, source.Street, source?.StreetNumber ?? string.Empty)
    };
}