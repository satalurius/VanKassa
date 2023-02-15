using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using VanKassa.Backend.Core.Data.EmployeesSort;
using VanKassa.Backend.Core.Services;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Exceptions;
using Xunit.Abstractions;

namespace VanKassa.Backend.Core.Tests.FunctionalTests;

public class EmployeeServiceFunctionalTests
{
    private readonly ITestOutputHelper testOutputHelper;

    private readonly Mock<IImageService> imageService;
    private readonly IMapper mapper = AutoMapperCreator.Create();

    private readonly SortEmployeesExecutor sortEmployeesExecutor = new();

    public EmployeeServiceFunctionalTests(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
        imageService = MockImageServiceFactory.Create();
    }

    [Fact]
    public async Task EmployeesService_GetEmployeesAsync_ReturnEmployees()
    {
        // Arrange
        MockDbFactory mockDbFactory = new();

        Mock<IDbContextFactory<VanKassaDbContext>> mockDbFactoryContext = mockDbFactory.Create();

        await SeedDbAsync(mockDbFactory.DbOptions);

        IEmployeesService employeesService = new EmployeesService(mockDbFactoryContext.Object,
            imageService.Object, sortEmployeesExecutor, mapper);

        // Act
        var result = await employeesService.GetEmployeesAsync();
        result = result.ToList();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<List<EmployeesDbDto>>();
    }

    [Fact]
    public async Task EmployeesService_GetEmployeesAsync_ThrowNotFound()
    {
        // Arrange
        MockDbFactory mockDbFactory = new();

        Mock<IDbContextFactory<VanKassaDbContext>> mockDbFactoryContext = mockDbFactory.Create();

        IEmployeesService employeesService = new EmployeesService(mockDbFactoryContext.Object,
            imageService.Object, sortEmployeesExecutor, mapper);

        // Act
        Func<Task> result = () => employeesService.GetEmployeesAsync();

        // Assert
        await result.Should().ThrowAsync<NotFoundException>();
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3 })]
    [InlineData(new[] { 5 })]
    public async Task EmployeesService_DeleteEmployeesAsync_NotThrowException(IEnumerable<int> deleteIds)
    {
        // Assert
        MockDbFactory mockDbFactory = new();

        Mock<IDbContextFactory<VanKassaDbContext>> mockDbFactoryContext = mockDbFactory.Create();

        await SeedDbAsync(mockDbFactory.DbOptions);

        IEmployeesService employeesService = new EmployeesService(mockDbFactoryContext.Object,
            imageService.Object, sortEmployeesExecutor, mapper);

        // Act
        Func<Task> result = () => employeesService.DeleteEmployeesAsync(deleteIds);

        // Assert
        await result.Should().NotThrowAsync();
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4 })]
    public async Task EmployeesService_DeleteEmployeesAsync_ThrowBadRequest(IEnumerable<int> deleteIds)
    {
        // Arrange
        MockDbFactory mockDbFactory = new();

        Mock<IDbContextFactory<VanKassaDbContext>> mockDbFactoryContext = mockDbFactory.Create();

        IEmployeesService employeesService = new EmployeesService(mockDbFactoryContext.Object,
            imageService.Object, sortEmployeesExecutor, mapper);

        // Act
        Func<Task> result = () => employeesService.DeleteEmployeesAsync(deleteIds);

        // Assert
        await result.Should().ThrowAsync<BadRequestException>();
    }

    [Theory]
    [MemberData(nameof(GetPageParams))]
    public async Task EmployeesService_GetEmployeesWithFiltersAsync_ReturnPageEmployees(
        EmployeesPageParameters pageParams)
    {
        // Arrange
        MockDbFactory mockDbFactory = new();

        Mock<IDbContextFactory<VanKassaDbContext>> mockDbFactoryContext = mockDbFactory.Create();

        await SeedDbAsync(mockDbFactory.DbOptions);

        IEmployeesService employeesService = new EmployeesService(mockDbFactoryContext.Object,
            imageService.Object, sortEmployeesExecutor, mapper);

        // Act
        var result = await employeesService.GetEmployeesWithFiltersAsync(pageParams);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<PageEmployeesDto>();
    }

    [Fact]
    public async Task EmployeesService_GetEmployeesWithFiltersAsync_ThrowNotFound()
    {
        // Arrange
        var pageParams = new EmployeesPageParameters
        {
            Page = 1,
            PageSize = 25,
            FilterText = "First"
        };

        MockDbFactory mockDbFactory = new();

        Mock<IDbContextFactory<VanKassaDbContext>> mockDbFactoryContext = mockDbFactory.Create();

        IEmployeesService employeesService = new EmployeesService(mockDbFactoryContext.Object,
            imageService.Object, sortEmployeesExecutor, mapper);

        // Act
        Func<Task> result = () => employeesService.GetEmployeesWithFiltersAsync(pageParams);

        // Assert
        await result.Should().ThrowAsync<NotFoundException>();
    }

    private async Task SeedDbAsync(DbContextOptions<VanKassaDbContext> options)
    {
        await using var context = new VanKassaDbContext(options);

        if (await context.Employees.CountAsync() > 0 || await context.Outlets.CountAsync() > 0)
            return;

        var employeesData = DataCreators.CreateFakeDbEmployeesList(500).ToList();
        var outletsData = DataCreators.CreateFakeOutletsList().ToList();

        context.Outlets.AddRange(outletsData);
        context.Employees.AddRange(employeesData);

        await context.SaveChangesAsync();
    }

    public static IEnumerable<object[]> GetPageParams()
        => new List<object[]>
        {
            new object[]
            {
                new EmployeesPageParameters
                {
                    Page = 1,
                    PageSize = 25,
                    FilterText = "First"
                }
            },
            new object[]
            {
                new EmployeesPageParameters
                {
                    Page = 2,
                    PageSize = 50,
                    FilterText = "First"
                }
            },
            new object[]
            {
                new EmployeesPageParameters
                {
                    Page = 2,
                    PageSize = 100,
                    FilterText = "Surname"
                }
            },
        };
}