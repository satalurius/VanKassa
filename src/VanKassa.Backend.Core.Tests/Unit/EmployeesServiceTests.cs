using FluentAssertions;
using Moq;
using VanKassa.Backend.Core.Data.EmployeesSort;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Domain.Dtos.Employees;

namespace VanKassa.Backend.Core.Tests.Unit
{
    public class EmployeesServiceTests
    {
        private readonly Mock<IEmployeesService> employeesService;

        public EmployeesServiceTests()
        {

            employeesService = new Mock<IEmployeesService>();
        }

        [Fact]
        public void EmployeesService_GetEmployeesAsync_ReturnEmployeesList()
        {
            // Arrange
            var employeesDbDto = new List<EmployeesDbDto>
            {
                new()
                {
                    UserId = 1,
                    Addresses = "addresses",
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Patronymic = "Patronymic",
                    Photo = "photo_path",
                    RoleName = "Roe"
                },
                new()
                {
                    UserId = 2,
                    Addresses = "addresses",
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Patronymic = "Patronymic",
                    Photo = "photo_path",
                    RoleName = "Roe"
                },
            };
            employeesService.Setup(e => e.GetEmployeesAsync().Result)
                .Returns(employeesDbDto);

            // Act
            var result = employeesService.Object
                .GetEmployeesAsync().Result;

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().BeAssignableTo<List<EmployeesDbDto>>();
        }
        
        [Theory]
        [MemberData(nameof(GetPageParams))]
        public void EmployeesService_GetEmployeesWithFiltersAsync_ReturnPageEmployeesDto(EmployeesPageParameters pageParams, 
            int employeesCount)
        {
            // Arrange
            var employeesDboDtos = CreateFakeEmployeesList(employeesCount);

            var sortStrategy = new SortEmployeesExecutor().GetSortImplementationByColumn(pageParams.SortedColumn);

            
            if (string.IsNullOrEmpty(pageParams.FilterText))
            {
                employeesDboDtos = employeesDboDtos
                    .Where(emp => emp.RoleName == pageParams.FilterText ||
                                  emp.LastName.ToLower().Contains(pageParams.FilterText.ToLower()) ||
                                  emp.FirstName.ToLower().Contains(pageParams.FilterText.ToLower()) ||
                                  emp.Patronymic.ToLower().Contains(pageParams.FilterText.ToLower()) ||
                                  emp.Addresses.ToLower().Contains(pageParams.FilterText.ToLower())).ToList();
            }
            
            var employeesBySize = sortStrategy
                .SortEmployees(employeesDboDtos, pageParams.SortDirection)
                .Skip(pageParams.Page * pageParams.PageSize)
                .Take(pageParams.PageSize)
                .ToList();


            var pageEmpDto = new PageEmployeesDto
            {
                TotalCount = employeesBySize.Count,
                EmployeesDbDtos = employeesBySize
            };

            employeesService.Setup(e => e.GetEmployeesWithFiltersAsync(pageParams).Result)
                .Returns(pageEmpDto);

            // Act
            var result = employeesService.Object.GetEmployeesWithFiltersAsync(pageParams).Result;
            
            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<PageEmployeesDto>();
        }

        public static IEnumerable<object[]> GetPageParams()
            => new List<object[]>
            {
                new object[] {new EmployeesPageParameters
                {
                    Page = 1,
                    PageSize = 25,
                    FilterText = "First"
                }, 150},
                new object[] {new EmployeesPageParameters
                {
                    Page = 2,
                    PageSize = 50,
                    FilterText = "First"
                }, 400},
                new object[] {new EmployeesPageParameters
                {
                    Page = 2,
                    PageSize = 100,
                    FilterText = "Surname"
                }, 400},
            };
        
        private IEnumerable<EmployeesDbDto> CreateFakeEmployeesList(int size)
        {
            var employeesDbDtos = new List<EmployeesDbDto>();

            for (var i = 1; i < size; i++)
            {
                EmployeesDbDto addedEmp;
                if (i < size / 2)
                {
                    addedEmp = new EmployeesDbDto
                    {
                        UserId = i,
                        Addresses = "addresses",
                        FirstName = "FirstName",
                        LastName = "LastName",
                        Patronymic = "Patronymic",
                        Photo = "photo_path",
                        RoleName = "Roe"
                    };                    
                }
                else
                {
                    addedEmp = new EmployeesDbDto
                    {
                        UserId = i,
                        Addresses = "addresses",
                        FirstName = "NoFirstName",
                        LastName = "Surname",
                        Patronymic = "Patronymic",
                        Photo = "photo_path",
                        RoleName = "Roe"
                    };   
                }

                employeesDbDtos.Add(addedEmp);
            }

            return employeesDbDtos;
        }
    }
}