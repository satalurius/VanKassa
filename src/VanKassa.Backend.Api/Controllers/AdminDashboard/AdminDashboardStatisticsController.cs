using Microsoft.AspNetCore.Mvc;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Requests;
using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Backend.Api.Controllers.Base;
using VanKassa.Backend.Core.Services.Interface.AdminDashboard;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Dtos.AdminDashboard.Statistics.Requests;
using VanKassa.Domain.Dtos.AdminDashboard.Statistics.TopProductStatistic;
using VanKassa.Domain.Dtos.AdminDashboard.Statistics.Outlets;

namespace VanKassa.Backend.Api.Controllers.AdminDashboard
{
    [Authorize
       (
           AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
           Roles = Roles.SuperAndAdministratorRoles
       )
   ]
    [ApiController]
    [Route("api/statistics")]
    public class AdminDashboardStatisticsController : BaseController<IAdminDashboardStatisticService>
    {
        public AdminDashboardStatisticsController(IAdminDashboardStatisticService service) : base(service)
        {
        }

        [Route("orders/by_period")]
        [HttpGet]
        [ProducesResponseType(typeof(OrdersStatisticByPeriodDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStatisticOfSoldOrdersByPeriodAsync([FromQuery] GetOrdersByPeriodRequest request)
            => Ok(await Service.GetOrdersStatisticByPeriodAsync(request));

        [Route("money_for_month")]
        [HttpGet]
        [ProducesResponseType(typeof(MoneyForMonthDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMoneyForMonthAsync([FromQuery] MoneyForMonthRequest request)
            => Ok(await Service.GetMoneyForMonthAsync(request));

        [Route("orders/every_month")]
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<SoldOrderByMonthDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStatisticOfSoldOrdersByEveryMonth(
            [FromQuery] GetOrdersByEveryMonthRequest request)
            => Ok(await Service.GetOrdersStatisticByEveryMonth(request));


        [Route("outlets/rental_by_period")]
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<RentalOutletDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStatisticForRentalOutletByPeriodAsync([FromQuery] GetRentalOutletRequestDto request)
            => Ok(await Service.StatisticForRentalOutletByPeriodAsync(request));

        [Route("outlets/raiting")]
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<RaitingOutletDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRaitingOfOutletsByPeriodAsync([FromQuery] GetRaitingOutletsRequestDto request)
            => Ok(await Service.StatisticsForRaitingsOutletsByPeriodAsync(request));

        [Route("products/top")]
        [HttpGet]
        [ProducesResponseType(typeof(TopProductsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStatisticsForTopProductsAsync([FromQuery] GetTopProductsRequestDto request)
            => Ok(await Service.GetStatisticsForTopProductsByPriceAsync(request));
    }
}
