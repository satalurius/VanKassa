using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using VanKassa.Backend.Core.Services;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Dtos.Employees.Requests;
using VanKassa.Domain.Exceptions;

namespace VanKassa.Backend.Core.Tests.FunctionalTests;

public class EmployeesEditServiceFunctionalTests
{
    private readonly Mock<IImageService> imageService = MockImageServiceFactory.Create();
    private readonly IMapper mapper = AutoMapperCreator.Create();

    [Fact]
    public async Task EmployeesEditService_SaveEmployeeAsync_NotThrowError()
    {
        // Arrange
        var savedEmployee = new SavedEmployeeRequestDto
        {
            FirstName = "FirstName",
            LastName = "LastName",
            Patronymic = "Patronymic",
            Photo = "Photo",
            RoleId = 1,
            OutletsIds = new List<int> { 1, 2 }
        };
        
        MockDbFactory mockDbFactory = new();

        Mock<IDbContextFactory<VanKassaDbContext>> mockDbFactoryContext = mockDbFactory.Create();

        await SeedDbAsync(mockDbFactory.DbOptions);

        IEmployeeEditService employeeEditService = new EmployeeEditService(
            mockDbFactoryContext.Object,
            mapper,
            imageService.Object);
        
        // Act
        Func<Task> result = () => employeeEditService.SaveEmployeeAsync(savedEmployee);
        
        // Assert
        await result.Should().NotThrowAsync();
    }

    [Fact]
    public async Task EmployeesEditService_SaveEmployeeAsync_ThrowBadRequestIfOutletsOrRoleDoesNotExist()
    {
        // Arrange
        var savedEmployee = new SavedEmployeeRequestDto
        {
            FirstName = "FirstName",
            LastName = "LastName",
            Patronymic = "Patronymic",
            Photo = "Photo",
            RoleId = 1,
            OutletsIds = new List<int> { 1, 2 }
        };
        
        MockDbFactory mockDbFactory = new();

        Mock<IDbContextFactory<VanKassaDbContext>> mockDbFactoryContext = mockDbFactory.Create();

        IEmployeeEditService employeeEditService = new EmployeeEditService(
            mockDbFactoryContext.Object,
            mapper,
            imageService.Object);
        
        // Act
        Func<Task> result = () => employeeEditService.SaveEmployeeAsync(savedEmployee);
        
        // Arrange
        await result.Should().ThrowAsync<BadRequestException>();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task EmployeesEditService_GetEditedEmployeeByIdAsync_ReturnEditedEmployee(int id)
    {
        // Arrange
        MockDbFactory mockDbFactory = new();

        Mock<IDbContextFactory<VanKassaDbContext>> mockDbFactoryContext = mockDbFactory.Create();

        await SeedDbAsync(mockDbFactory.DbOptions);

        IEmployeeEditService employeeEditService = new EmployeeEditService(
            mockDbFactoryContext.Object,
            mapper,
            imageService.Object);
        
        // Act
        var result = await employeeEditService.GetEditedEmployeeByIdAsync(id);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<EditedEmployeeDto>();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task EmployeesEditService_GetEditedEmployeeByIdAsync_ThrowNotFoundIfEmployeeNotExist(int id)
    {
        // Arrange
        MockDbFactory mockDbFactory = new();

        Mock<IDbContextFactory<VanKassaDbContext>> mockDbFactoryContext = mockDbFactory.Create();

        IEmployeeEditService employeeEditService = new EmployeeEditService(
            mockDbFactoryContext.Object,
            mapper,
            imageService.Object);
        
        // Act
        Func<Task> result = () => employeeEditService.GetEditedEmployeeByIdAsync(id);

        // Asert
        await result.Should().ThrowAsync<NotFoundException>();
    }
    
    private async Task SeedDbAsync(DbContextOptions<VanKassaDbContext> options)
    {
        await using var context = new VanKassaDbContext(options);

        if (await context.Employees.AnyAsync() || await context.Outlets.AnyAsync())
            return;

        var employeesData = DataCreators.CreateFakeDbEmployeesList(500).ToList();
        var outletsData = DataCreators.CreateFakeOutletsList().ToList();

        context.Employees.AddRange(employeesData);
        context.Outlets.AddRange(outletsData);
        await context.SaveChangesAsync();
    }
}
