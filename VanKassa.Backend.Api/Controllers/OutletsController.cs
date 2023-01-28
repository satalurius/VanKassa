using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using VanKassa.Backend.Api.Controllers.Base;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Dtos;

namespace VanKassa.Backend.Api.Controllers
{
    [EnableCors(PolicyConstants.WebPolicy)]
    [ApiController]
    [Route("api/outlets")]
    public class OutletsController : BaseController<IOutletService>
    {
        public OutletsController(IOutletService outletService) : base(outletService)
        {
        }

        /// <summary>
        /// Get outlets
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns if outlets exits</response>
        /// <response code="204">Returns if outlets not found</response>
        [Route("all")]
        [HttpGet]
        [ProducesResponseType(typeof(OutletDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OutletDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOutletsAsync()
        {
            try
            {
                var outlets = await Service.GetOutletsAsync();

                if (outlets is null)
                    return BadRequest(Array.Empty<OutletDto>());

                return Ok(outlets);
            }
            catch (InvalidOperationException)
            {
                return BadRequest(Array.Empty<OutletDto>());
            }
        }
    }
}
