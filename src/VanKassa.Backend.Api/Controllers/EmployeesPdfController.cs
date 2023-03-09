using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Razor.Templating.Core;
using VanKassa.Backend.Api.Controllers.Base;
using VanKassa.Backend.Core.Services.Interface;
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
    
    [HttpPost]
    public async Task<IActionResult> GeneratePdf([FromBody] GeneratePdfEmployeesRequest pdfRequest)
    {
        var pdfEmployeesViewModel = mapper.Map<PdfEmployeesViewModel>(pdfRequest);
       
        var html = await razorTemplateEngine.RenderAsync("~/Views/EmployeesPdfTemplate.cshtml", pdfEmployeesViewModel);

        var report = await Service.GenerateReportFromHtmlAsync(html);
        
        return File(report.FileStream, report.ContentType, report.Name);
    }
}