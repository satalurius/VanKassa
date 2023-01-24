using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using VanKassa.Backend.Api.Controllers;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Dtos.Employees.Requests;

namespace VanKassa.Backend.Api.Tests.Controllers
{
    public class EmployeesControllerTests
    {
        private readonly IEmployeesService employeesService;
        private readonly IEmployeeEditService employeeEditService;

        private readonly EmployeesController employeesController;

        public EmployeesControllerTests()
        {
            employeesService = A.Fake<IEmployeesService>();
            employeeEditService = A.Fake<IEmployeeEditService>();

            employeesController = new EmployeesController(employeesService, employeeEditService);
        }

        [Fact]
        public void EmployeesController_GetEmployeesAsync_ReturnOkObject()
        {
            // Arrange
            var pageParams = new EmployeesPageParameters();
            List<EmployeesDbDto> empDtos = A.Fake<List<EmployeesDbDto>>();
            A.CallTo(() => employeesService.GetEmployeesAsync()).Returns(empDtos);

            // Act
            var result = employeesController.GetEmployeesAsync(pageParams).Result;
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void EmployeesController_DeleteEmployeesByIdAsync_ReturnOk()
        {
            // Arrange
            var deletIds = new List<int>() { 1, 2, 3, 4 };
            A.CallTo(() => employeesService.DeleteEmployeesAsync(deletIds));

            // Act
            var result = employeesController.DeleteEmployeesByIdAsync(deletIds).Result;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkResult));
        }

        [Fact]
        public void EmployeesController_GetEditedEmployeeByIdAsync_ReturnOkObject()
        {
            // Arrange
            var empId = 1;
            var editEmp = A.Fake<EditedEmployeeDto>();
            A.CallTo(() => employeeEditService.GetEditedEmployeeByIdAsync(empId)).Returns(editEmp);

            // Act
            var result = employeesController.GetEditedEmployeeByIdAsync(empId).Result;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void EmployeesController_ChangeEmployeeAsync_ReturnOk()
        {
            // Arrange
            var editEmp = A.Fake<ChangedEmployeeRequestDto>();
            A.CallTo(() => employeeEditService.ChangeEmployeeAsync(editEmp));

            // Act
            var result = employeesController.ChangeEmployeeAsync(editEmp).Result;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkResult));
        }

        [Fact]
        public void EmployeesController_SaveEmployeeAsync_ReturnOk()
        {
            // Arrange
            var savedEmp = A.Fake<SavedEmployeeRequestDto>();
            A.CallTo(() => employeeEditService.SaveEmployeeAsync(savedEmp));
            // Act
            var result = employeesController.SaveEmployeeAsync(savedEmp).Result;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkResult));
        }

    }
}
