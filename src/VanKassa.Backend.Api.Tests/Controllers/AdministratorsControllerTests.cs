using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VanKassa.Backend.Api.Controllers;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Domain.Dtos.Admins.Requests;
using VanKassa.Domain.Exceptions;

namespace VanKassa.Backend.Api.Tests.Controllers;
public class AdministratorsControllerTests
{
    private readonly Mock<IAdministratorsService> administratorsService = new();

    [Fact]
    public async Task AdministratorsController_CreateAdministratorAsync_ReturnOkObject()
    {
        // Arrange
        var createAdministrator = new CreateAdministratorRequest();

        administratorsService.Setup(e => e.CreateAdministratorAsync(createAdministrator))
            .Returns(Task.CompletedTask);

        var controller = new AdministratorsController(administratorsService.Object);

        // Act
        var result = await controller.CreateAdministratorAsync(createAdministrator);

        // Assert
        result.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task AdministratorsController_CreateAdministratorAsync_ThrowBadRequestIfFailed()
    {
        // Arrange
        var createAdministrator = new CreateAdministratorRequest();

        administratorsService.Setup(e => e.CreateAdministratorAsync(createAdministrator))
            .ThrowsAsync(new BadRequestException("Create was failed"));

        var controller = new AdministratorsController(administratorsService.Object);


        // Act
        var result = async () => await controller.CreateAdministratorAsync(createAdministrator);

        // Assert
        await result.Should().ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async Task AdministratorsController_DeleteAdministratorsAsync_ReturnOkResult()
    {
        // Arrange
        var deletedId = 1;

        administratorsService.Setup(e => e.DeleteAdministratorAsync(deletedId))
            .Returns(Task.CompletedTask);

        var controller = new AdministratorsController(administratorsService.Object);

        // Act
        var result = await controller.DeleteAdministratorAsync(deletedId);

        // Assert
        result.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task AdministratorsController_DeleteAdministratorsAsync_ThrowNotFound()
    {
        // Arrange
        var deletedId = 1;

        administratorsService.Setup(e => e.DeleteAdministratorAsync(deletedId))
            .ThrowsAsync(new NotFoundException());

        var controller = new AdministratorsController(administratorsService.Object);

        // Act
        var result = async () => await controller.DeleteAdministratorAsync(deletedId);

        // Assert
        await result.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task AdministratorsController_ChangeAdministratorsAsync_ReturnOkResult()
    {
        // Arrange
        var changeAdministrator = new ChangeAdministratorRequest();

        administratorsService.Setup(e => e.ChangeAdministratorAsync(changeAdministrator))
            .Returns(Task.CompletedTask);

        var controller = new AdministratorsController(administratorsService.Object);

        // Act
        var result = await controller.ChangeAdministratorAsync(changeAdministrator);

        // Assert
        result.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task AdministratorsController_ChangeAdministratorsAsync_ThrowBadRequestExceptionIfError()
    {
        // Arrange
        var changeAdministrator = new ChangeAdministratorRequest();

        administratorsService.Setup(e => e.ChangeAdministratorAsync(changeAdministrator))
            .ThrowsAsync(new BadRequestException("Operation was failed"));

        var controller = new AdministratorsController(administratorsService.Object);

        // Act
        var result = async () => await controller.ChangeAdministratorAsync(changeAdministrator);

        // Assert
        await result.Should().ThrowAsync<BadRequestException>();
    }

}
