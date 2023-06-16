using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VanKassa.Backend.Api.Controllers.AdminDashboard;
using VanKassa.Backend.Core.Services.Interface.AdminDashboard;
using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Requests;
using VanKassa.Domain.Dtos.AdminDashboard.Statistics.Outlets;
using VanKassa.Domain.Dtos.AdminDashboard.Statistics.Requests;
using VanKassa.Domain.Dtos.AdminDashboard.Statistics.TopProductStatistic;

namespace VanKassa.Backend.Api.Tests.Controllers.AdminDashboard;
public class AdminDashboardStatisticsControllerTests
{
    private readonly Mock<IAdminDashboardStatisticService> adminDashboardStatisticService = new();

    [Fact]
    public async Task AdminDashboardStatisticsController_GetStatisticOfSoldOrdersByPeriodAsync_ReturnOkOrderStatistic()
    {
        // Arrange
        var startDate = DateTime.Now.AddDays(-1);
        var endDate = DateTime.Now.AddDays(1);

        var getOrders = new GetOrdersByPeriodRequest
        {
            StartDate = startDate,
            EndDate = endDate
        };

        var statistic = new OrdersStatisticByPeriodDto
        {
            Count = 10,
            StartDate = startDate,
            EndDate = endDate,
            TotalMoney = 1000
        };

        adminDashboardStatisticService.Setup(e => e.GetOrdersStatisticByPeriodAsync(getOrders))
            .ReturnsAsync(statistic);

        var controller = new AdminDashboardStatisticsController(adminDashboardStatisticService.Object);

        // Act
        var result = await controller.GetStatisticOfSoldOrdersByPeriodAsync(getOrders);
        var value = ((OkObjectResult)result).Value;

        // Assert
        value.Should().BeEquivalentTo(statistic);
        value.Should().BeOfType<OrdersStatisticByPeriodDto>();

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task AdminDashboardStatisticsController_GetMoneyForMonthAsync_ReturnOkMoneyForMonthDto()
    {
        // Arrange
        var request = new MoneyForMonthRequest { MonthDate = DateTime.Now };
        var moneyForMonth = new MoneyForMonthDto
        {
            Money = 1000
        };

        adminDashboardStatisticService.Setup(e => e.GetMoneyForMonthAsync(request))
            .ReturnsAsync(moneyForMonth);

        var controller = new AdminDashboardStatisticsController(adminDashboardStatisticService.Object);

        // Act
        var result = await controller.GetMoneyForMonthAsync(request);
        var value = ((OkObjectResult)result).Value;

        // Assert
        value.Should().BeEquivalentTo(moneyForMonth);
        value.Should().BeOfType<MoneyForMonthDto>();

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task AdminDashboardStatisticsController_GetStatisticOfSoldOrdersByEveryMonth_ReturnOkListSoldOrers()
    {
        // Arrange
        var request = new GetOrdersByEveryMonthRequest { YearDate = DateTime.Now.Year };
        IList<SoldOrderByMonthDto> soldOrderByMonthDtos = new List<SoldOrderByMonthDto>();

        adminDashboardStatisticService.Setup(e => e.GetOrdersStatisticByEveryMonth(request))
          .ReturnsAsync(soldOrderByMonthDtos);

        var controller = new AdminDashboardStatisticsController(adminDashboardStatisticService.Object);

        // Act
        var result = await controller.GetStatisticOfSoldOrdersByEveryMonth(request);
        var value = ((OkObjectResult)result).Value;

        // Assert
        value.Should().BeEquivalentTo(soldOrderByMonthDtos);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task AdminDashboardStatisticsController_GetStatisticForRentalOutletByPeriodAsync_ReturnOkListRental()
    {
        // Arrange
        var startDate = DateTime.Now.AddDays(-1);
        var endDate = DateTime.Now.AddDays(1);

        var request = new GetRentalOutletRequestDto
        {
            StartDate = startDate,
            EndDate = endDate,
        };

        IList<RentalOutletDto> rentals = new List<RentalOutletDto>();

        adminDashboardStatisticService.Setup(e => e.StatisticForRentalOutletByPeriodAsync(request))
            .ReturnsAsync(rentals);

        var controller = new AdminDashboardStatisticsController(adminDashboardStatisticService.Object);

        // Act
        var result = await controller.GetStatisticForRentalOutletByPeriodAsync(request);
        var value = ((OkObjectResult)result).Value;

        // Assert
        value.Should().BeEquivalentTo(rentals);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task AdminDashboardStatisticsController_GetRaitingOfOutletsByPeriodAsync_ReturnOkListRaiting()
    {
        // Arrange
        var startDate = DateTime.Now.AddDays(-1);
        var endDate = DateTime.Now.AddDays(1);

        var request = new GetRaitingOutletsRequestDto
        {
            StartDate = startDate,
            EndDate = endDate,
            Positions = 10
        };

        IList<RaitingOutletDto> raitings = new List<RaitingOutletDto>();

        adminDashboardStatisticService.Setup(e => e.StatisticsForRaitingsOutletsByPeriodAsync(request))
            .ReturnsAsync(raitings);

        var controller = new AdminDashboardStatisticsController(adminDashboardStatisticService.Object);

        // Act
        var result = await controller.GetRaitingOfOutletsByPeriodAsync(request);
        var value = ((OkObjectResult)result).Value;

        // Assert
        value.Should().BeEquivalentTo(raitings);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task AdminDashboardStatisticsController_GetStatisticsForTopProductsAsync_ReturnOkTopProducts()
    {
        // Arrange
        var startDate = DateTime.Now.AddDays(-1);
        var endDate = DateTime.Now.AddDays(1);

        var request = new GetTopProductsRequestDto
        {

            StartDate = startDate,
            EndDate = endDate,
            Positions = 10
        };

        var topProduct = new TopProductsDto();

        adminDashboardStatisticService.Setup(e => e.GetStatisticsForTopProductsByPriceAsync(request))
            .ReturnsAsync(topProduct);

        var controller = new AdminDashboardStatisticsController(adminDashboardStatisticService.Object);

        // Act
        var result = await controller.GetStatisticsForTopProductsAsync(request);
        var value = ((OkObjectResult)result).Value;

        // Assert
        value.Should().BeEquivalentTo(topProduct);
        value.Should().BeOfType<TopProductsDto>();

        result.Should().BeOfType<OkObjectResult>();
    }


}
