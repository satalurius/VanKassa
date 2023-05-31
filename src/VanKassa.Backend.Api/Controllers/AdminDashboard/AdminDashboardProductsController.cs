using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanKassa.Backend.Api.Controllers.Base;
using VanKassa.Backend.Core.Services.Interface.AdminDashboard;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Dtos.AdminDashboard.Orders;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Products;

namespace VanKassa.Backend.Api.Controllers.AdminDashboard;

[Authorize
    (
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Roles.SuperAndAdministratorRoles
    )
]
[ApiController]
[Route("/api/admin_dashboard_products")]
public class AdminDashboardProductsController : BaseController<IAdminDashboardProductsService>
{
    public AdminDashboardProductsController(IAdminDashboardProductsService service) : base(service)
    {
    }

    /// <summary>
    /// Create Product
    /// </summary>
    /// <response code="200">Return if create was success</response>
    /// <response code="400">Return if create failed</response>
    [Route("create")]
    [HttpPost]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductDto request)
    {
        await Service.CreateProductAsync(request);

        return Ok();
    }

    
    /// <summary>
    /// Get all products by filter
    /// </summary>
    [Route("all")]
    [HttpGet]
    [ProducesResponseType(typeof(PageProductsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductsByFilterAsync([FromQuery] ProductsFilterDto parameters)
        => Ok(
            await Service.GetProductsByFilterAsync(parameters)
        );

    [Route("delete")]
    [HttpDelete]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteProductByIdAsync([FromQuery] int deletedId)
    {
        await Service.DeleteProductAsync(deletedId);
        
        return Ok();
    }

    [Route("update")]
    [HttpPut]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProductAsync([FromBody] UpdateProductDto request)
    {
        await Service.UpdateProductAsync(request);

        return Ok();
    }
}