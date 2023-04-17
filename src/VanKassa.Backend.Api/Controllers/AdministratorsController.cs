using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanKassa.Backend.Api.Controllers.Base;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Dtos.Admins;
using VanKassa.Domain.Dtos.Admins.Requests;

namespace VanKassa.Backend.Api.Controllers
{
    [
        Authorize
        (
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = Roles.SuperAdministrator
        )
    ]
    [ApiController]
    [Route("api/administrators")]
    public class AdministratorsController : BaseController<IAdministratorsService>
    {
        public AdministratorsController(IAdministratorsService service) : base(service)
        {
        }

        /// <summary>
        /// Create Administrator
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Return if create was success</response>
        /// <response code="400">Return if create failed</response>
        [Route("create")]
        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAdministratorAsync([FromBody] CreateAdministratorRequest request)
        {
            await Service.CreateAdministratorAsync(request);

            return Ok();
        }

        /// <summary>
        /// Get all administrators
        /// </summary>
        /// <response code="200">Returns if exits</response>
        /// <response code="404">Returns if not found</response>
        [Route("all")]
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<AdministratorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAdministratorsAsync()
            => Ok(
                await Service.GetAdministratorsAsync()
            );

        /// <summary>
        /// Delete Sended Administrator
        /// </summary>
        /// <response code="200">Returns if employees successfully deleted</response>
        /// <response code="400">Returns if errors occur while delete</response>
        [Route("delete")]
        [HttpDelete]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAdministratorAsync([FromQuery] int deleteId)
        {
            await Service.DeleteAdministratorAsync(deleteId);
            return Ok();
        }

        /// <summary>
        /// Edit Sended Admin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("edit")]
        [HttpPut]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeAdministratorAsync([FromBody] ChangeAdministratorRequest request)
        {
            await Service.ChangeAdministratorAsync(request);
            return Ok();
        }

    }
}
