using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Razor.Templating.Core;
using VanKassa.Backend.Api.Controllers.Base;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Dtos.Employees.Requests;
using VanKassa.Domain.ViewModels;

namespace VanKassa.Backend.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/employees_pdf")]
public class EmployeesPdfController : BaseController<IEmployeesPdfService>
{
    private readonly IRazorTemplateEngine razorTemplateEngine;
    private readonly IMapper mapper;

    public EmployeesPdfController(IEmployeesPdfService employeesPdfService, IRazorTemplateEngine razorTemplateEngine, IMapper mapper) 
        : base(employeesPdfService)
    {
        this.razorTemplateEngine = razorTemplateEngine;
        this.mapper = mapper;
    }

    /// <summary>
    /// Return Employees list for pdf page.
    /// </summary>
    /// <response code="200">Return if data was found</response>
    /// <response code="404">Return if not found</response>
    [HttpGet]
    [ProducesResponseType(typeof(IList<PdfEmployeeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmployeesAsync()
        => Ok(
            await Service.GetEmployeesAsync()
        );

    /// <summary>
    /// Generate PDF report on all employees.
    /// </summary>
    /// <param name="pdfRequest">Outlet property can be empty if report for all employees.</param>
    /// <response code="200">Returns pdf file if was generated</response>
    /// <response code="500">Returns if some error occured</response>
    [HttpPost]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Generate([FromBody] GeneratePdfEmployeesRequest pdfRequest)
    {
        var pdfEmployeesViewModel = mapper.Map<PdfEmployeesViewModel>(pdfRequest);
       
        var html = await razorTemplateEngine.RenderAsync("~/Views/EmployeesPdfTemplate.cshtml", pdfEmployeesViewModel);

        var report = await Service.GenerateReportFromHtmlAsync(html);
        
        return File(report.FileStream, report.ContentType, report.Name);
    }
}