
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VanKassa.Backend.Api.Controllers.AdminDashboard;
using VanKassa.Backend.Core.Services.Interface.AdminDashboard;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Categories;
using VanKassa.Domain.Exceptions;

namespace VanKassa.Backend.Api.Tests.Controllers.AdminDashboard;
public class AdminDashboardCategoriesTests
{
    private readonly Mock<IAdminDashboardCategoriesService> adminDashBoardCategoriesService = new();

    [Fact]
    public async Task AdminDashboardCategories_CreateCategoryAsync_ReturnOkResult()
    {
        // Arrange 
        var createCategory = new CreateCategoryDto
        {
            Name = "Категория",
        };

        adminDashBoardCategoriesService.Setup(e => e.CreateCategoryAsync(createCategory))
            .Returns(Task.CompletedTask);

        var controller = new AdminDashboardCategoriesController(adminDashBoardCategoriesService.Object);

        // Act
        var result = await controller.CreateCategoryAsync(createCategory);

        // Assert
        result.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task AdminDashboardCategories_CreateCategoryAsync_ThrowBadRequestIfCategoryExists()
    {
        // Arrange
        var createCategory = new CreateCategoryDto
        {
            Name = "Категория",
        };

        adminDashBoardCategoriesService.Setup(e => e.CreateCategoryAsync(createCategory))
            .ThrowsAsync(new BadRequestException("Category arleady exists"));

        var controller = new AdminDashboardCategoriesController(adminDashBoardCategoriesService.Object);

        // Act
        var result = async () => await controller.CreateCategoryAsync(createCategory);

        // Assert
        await result.Should().ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async Task AdminDashboardCategories_GetCategoriesAsync_ReturnCategoriesOkResult()
    {
        // Arrange
        var categories = new PageCategoryDto()
        {
            Categories = new List<CategoryDto>()
        };

        adminDashBoardCategoriesService.Setup(e => e.GetCategoriesAsync())
            .ReturnsAsync(categories);

        var controller = new AdminDashboardCategoriesController(adminDashBoardCategoriesService.Object);

        // Act
        var result = await controller.GetCategoriesAsync();
        var value = ((OkObjectResult)result).Value;

        // Assert
        value.Should().BeEquivalentTo(categories);
        value.Should().BeOfType<PageCategoryDto>();

        result.Should().BeOfType<OkObjectResult>();
    }


    [Fact]
    public async Task AdminDashboardCategories_DeleteProductByIdAsync_ReturnOkResult()
    {
        // Arrange 
        var deletedId = 1;

        adminDashBoardCategoriesService.Setup(e => e.DeleteCategoryAsync(deletedId))
            .Returns(Task.CompletedTask);

        var controller = new AdminDashboardCategoriesController(adminDashBoardCategoriesService.Object);

        // Act
        var result = await controller.DeleteProductByIdAsync(deletedId);

        // Assert
        result.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task AdminDashboardCategories_DeleteProductByIdAsync_ThrowNotFoundIfProductNotExist()
    {
        // Arrange
        var deletedId = 1;

        adminDashBoardCategoriesService.Setup(e => e.DeleteCategoryAsync(deletedId))
            .ThrowsAsync(new NotFoundException());

        var controller = new AdminDashboardCategoriesController(adminDashBoardCategoriesService.Object);

        // Act
        var result = async () => await controller.DeleteProductByIdAsync(deletedId);

        // Assert
        await result.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task AdminDashboardCategories_UpdateCategoryAsync_ReturnOkResult()
    {
        // Arrange
        var category = new CategoryDto { CategoryId = 1, Name = "Новая категория" };

        adminDashBoardCategoriesService.Setup(e => e.UpdateCategoryAsync(category))
            .Returns(Task.CompletedTask);

        var controller = new AdminDashboardCategoriesController(adminDashBoardCategoriesService.Object);

        // Act
        var result = await controller.UpdateCategoryAsync(category);

        // Assert
        result.Should().BeOfType<OkResult>();
    }
}
