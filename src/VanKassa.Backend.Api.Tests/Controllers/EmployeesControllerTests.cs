using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VanKassa.Backend.Api.Controllers;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Dtos.Employees.Requests;
using VanKassa.Domain.Exceptions;

namespace VanKassa.Backend.Api.Tests.Controllers
{
    public class EmployeesControllerTests
    {
        private readonly Mock<IEmployeesService> employeesService = new();
        private readonly Mock<IEmployeeEditService> employeeEditService = new();

        [Fact]
        public async Task EmployeesController_GetEmployeesAsync_ReturnEmployeesListOkResult()
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

            var pageEmp = new PageEmployeesDto
            {
                EmployeesDbDtos = employeesDbDto,
                TotalCount = employeesDbDto.Count
            };

            var parameters = new EmployeesPageParameters();

            var controller = new EmployeesController(employeesService.Object, employeeEditService.Object);

            employeesService.Setup(e => e.GetEmployeesWithFiltersAsync(parameters))
                .ReturnsAsync(pageEmp);

            // Act
            var result = await controller.GetEmployeesAsync(parameters);

            var value = ((OkObjectResult)result).Value;

            // Assert
            value.Should().BeEquivalentTo(pageEmp);
            value.Should().BeOfType<PageEmployeesDto>();

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task EmployeesController_DeleteEmployeesByIdAsync_ReturnOkResult()
        {
            // Arrange
            var deletedIds = new List<int>() { 1, 2, 3 };

            var controller = new EmployeesController(employeesService.Object, employeeEditService.Object);

            employeesService.Setup(e => e.DeleteEmployeesAsync(deletedIds))
                .Returns(Task.CompletedTask);

            // Act
            var result = await controller.DeleteEmployeesByIdAsync(deletedIds);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task EmployeesController_DeleteEmployeesByIdAsync_ThrowNotFoundIfNotExist()
        {
            // Arrange
            var deletedIds = new List<int>() { 1, 2, 3 };

            var controller = new EmployeesController(employeesService.Object, employeeEditService.Object);

            employeesService.Setup(e => e.DeleteEmployeesAsync(deletedIds))
                .ThrowsAsync(new NotFoundException());

            // Act

            var result = async () => await controller.DeleteEmployeesByIdAsync(deletedIds);

            // Assert

            await result.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task EmployeesController_GetEditedEmployeeByIdAsync_ReturnEditedEmployeeOkResult()
        {
            // Arrange
            var editedEmployee = new EditedEmployeeDto()
            {
                UserId = 1,
                FirstName = "FirstName"
            };

            var employeeId = 1;

            var controller = new EmployeesController(employeesService.Object, employeeEditService.Object);

            employeeEditService.Setup(e => e.GetEditedEmployeeByIdAsync(employeeId))
                .ReturnsAsync(editedEmployee);

            // Act

            var result = await controller.GetEditedEmployeeByIdAsync(employeeId);
            var value = ((OkObjectResult)result).Value;

            // Assert

            value.Should().BeEquivalentTo(editedEmployee);
            value.Should().BeOfType<EditedEmployeeDto>();

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task EmployeesController_GetEditedEmployeeByIdAsync_ThrowNotFoundExceptionIfEmployeeWasNotFound()
        {
            // Arrange
            var employeeId = 1;

            var exception = new NotFoundException();

            var controller = new EmployeesController(employeesService.Object, employeeEditService.Object);

            employeeEditService.Setup(e => e.GetEditedEmployeeByIdAsync(employeeId))
                .ThrowsAsync(exception);

            // Act
            var result = async () => await controller.GetEditedEmployeeByIdAsync(employeeId);

            // Assert
            await result.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task EmployeesController_ChangeEmployeeAsync_ReturnOkResult()
        {
            // Arrange

            var changedEmp = new ChangedEmployeeRequestDto();

            var controller = new EmployeesController(employeesService.Object, employeeEditService.Object);

            employeeEditService.Setup(e => e.ChangeEmployeeAsync(changedEmp))
                .Returns(Task.CompletedTask);

            // Act
            var result = await controller.ChangeEmployeeAsync(changedEmp);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task EmployeesController_ChangeEmployeeAsync_ThrowsNotFound()
        {
            // Arrange
            var changedEmp = new ChangedEmployeeRequestDto();

            var controller = new EmployeesController(employeesService.Object, employeeEditService.Object);

            employeeEditService.Setup(e => e.ChangeEmployeeAsync(changedEmp))
                .ThrowsAsync(new NotFoundException());

            // Act
            var result = async () => await controller.ChangeEmployeeAsync(changedEmp);

            // Assert
            await result.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task EmployeesController_SaveEmployeeAsync_ReturnOkResult()
        {
            // Arrange
            var savedEmployee = new SavedEmployeeRequestDto();

            var controller = new EmployeesController(employeesService.Object, employeeEditService.Object);

            employeeEditService.Setup(e => e.SaveEmployeeAsync(savedEmployee))
                .Returns(Task.CompletedTask);

            // Act
            var result = await controller.SaveEmployeeAsync(savedEmployee);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task EmployeesController_SaveEmployeeAsync_ThrowsBadRequestIfSaveWasFailed()
        {
            // Arrange
            var savedEmployee = new SavedEmployeeRequestDto();

            var controller = new EmployeesController(employeesService.Object, employeeEditService.Object);

            employeeEditService.Setup(e => e.SaveEmployeeAsync(savedEmployee))
                .ThrowsAsync(new BadRequestException("Save was failed"));

            // Act
            var result = async () => await controller.SaveEmployeeAsync(savedEmployee);

            // Assert
            await result.Should().ThrowAsync<BadRequestException>();
        }
    }
}
