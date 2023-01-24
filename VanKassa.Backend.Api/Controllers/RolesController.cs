using Microsoft.AspNetCore.Mvc;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Domain.Dtos.Employees;

namespace VanKassa.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly IEmployeesRoleService employeesRoleService;

        public RolesController(IEmployeesRoleService employeesRoleService)
        {
            this.employeesRoleService = employeesRoleService;
        }

        /// <summary>
        /// Get outlets
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns if roles exits</response>
        /// <response code="204">Returns if roles not found</response>
        [Route("all")]
        [HttpGet]
        [ProducesResponseType(typeof(EmployeesRoleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmployeesRoleDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRolesAsync()
        {
            try
            {
                var roles = await employeesRoleService.GetAllRolesAsync();

                if (roles is null)
                    return BadRequest(Array.Empty<EmployeesRoleDto>());

                return Ok(roles);
            }
            catch (InvalidOperationException)
            {
                return BadRequest(Array.Empty<EmployeesRoleDto>());
            }
        }
    }
}
