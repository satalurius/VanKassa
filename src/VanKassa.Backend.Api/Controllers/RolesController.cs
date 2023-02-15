using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using VanKassa.Backend.Api.Controllers.Base;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Dtos.Employees;

namespace VanKassa.Backend.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/roles")]
    public class RolesController : BaseController<IEmployeesRoleService>
    {
        public RolesController(IEmployeesRoleService employeesRoleService) : base(employeesRoleService)
        {
        }

        /// <summary>
        /// Get roles
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns if roles exits</response>
        /// <response code="204">Returns if roles not found</response>
        [HttpGet]
        [ProducesResponseType(typeof(EmployeesRoleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRolesAsync()
            => Ok(
                await Service.GetAllRolesAsync()
            );
    }
}