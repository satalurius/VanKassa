using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanKassa.Backend.Api.Controllers.Base;
using VanKassa.Backend.Core.Services.Interface.AdminDashboard;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Requests;

namespace VanKassa.Backend.Api.Controllers.AdminDashboard;

[Authorize
    (
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Roles.SuperAndAdministratorRoles
    )
]
[ApiController]
[Route("/api/orders")]
public class OrdersController : BaseController<IOrdersService>
{
    public OrdersController(IOrdersService service) : base(service)
    {
    }

    [Route("create")]
    [HttpPost]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderRequestDto createOrderRequestDto)
    {
        await Service.CreateOrderAsync(createOrderRequestDto);
        
        return Ok();
    }


    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrdersByFilterAsync([FromQuery] OrdersPageParameters parameters)
        => Ok(await Service.GetOrderByFilterAsync(parameters));

    [Route("by_period")]
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<OrdersStatisticByPeriodDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStatisticOfSoldOrdersByPeriodAsync([FromQuery] GetOrdersByPeriodRequest request)
        => Ok(await Service.GetOrdersStatisticByPeriodAsync(request));

    [Route("every_month")]
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<SoldOrderByMonthDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetStatisticOfSoldOrdersByEveryMonth(
        [FromQuery] GetOrdersByEveryMonthRequest request)
        => Ok(await Service.GetOrdersStatisticByEveryMonth(request));
}