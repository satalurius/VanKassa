using ClosedXML.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VanKassa.Backend.Api.Controllers.Base;
using VanKassa.Backend.Core.Services.Interface.AdminDashboard;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Dtos.AdminDashboard.Orders.Requests.OrdersReportRequest;

namespace VanKassa.Backend.Api.Controllers.AdminDashboard;

[Authorize
    (
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Roles.SuperAndAdministratorRoles
    )
]
[ApiController]
[Route("/api/reports")]
public class AdminDashboardReportsController : BaseController<IAdminDashboardReportsService>
{
    public AdminDashboardReportsController(IAdminDashboardReportsService service) : base(service)
    {
    }

    [HttpPost]
    [Route("orders")]
    public async Task<IActionResult> GenerateOrdersReportAsync()
    {
        var workBook = await Service.GenerateOrdersReportAsync();

        return File(workBook.Content, workBook.ContentType , workBook.FileName);
    }
}
