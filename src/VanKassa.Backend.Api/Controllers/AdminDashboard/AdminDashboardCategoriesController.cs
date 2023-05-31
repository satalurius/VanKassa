using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanKassa.Backend.Api.Controllers.Base;
using VanKassa.Backend.Core.Services.Interface.AdminDashboard;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Categories;

namespace VanKassa.Backend.Api.Controllers.AdminDashboard;

[Authorize
    (
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Roles.SuperAndAdministratorRoles
    )
]
[ApiController]
[Route("/api/admin_dashboard_categories")]
public class AdminDashboardCategoriesController : BaseController<IAdminDashboardCategoriesService>
{
    public AdminDashboardCategoriesController(IAdminDashboardCategoriesService service) : base(service)
    {
    }

    /// <summary>
    /// Create Category
    /// </summary>
    /// <param name="request"></param>
    [Route("create")]
    [HttpPost]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryDto request)
    {
        await Service.CreateCategoryAsync(request);

        return Ok();
    }

    /// <summary>
    /// Get all categories
    /// </summary>
    [Route("all")]
    [HttpGet]
    [ProducesResponseType(typeof(PageCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoriesAsync()
        => Ok(
            await Service.GetCategoriesAsync()
        );

    [Route("delete")]
    [HttpDelete]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteProductByIdAsync([FromQuery] int deletedId)
    {
        await Service.DeleteCategoryAsync(deletedId);
        
        return Ok();
    }

    [Route("update")]
    [HttpPut]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCategoryAsync([FromBody] CategoryDto request)
    {
        await Service.UpdateCategoryAsync(request);

        return Ok();
    }
}