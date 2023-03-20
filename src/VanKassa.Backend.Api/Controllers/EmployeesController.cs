using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanKassa.Backend.Api.Controllers.Base;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Dtos.Employees.Requests;

namespace VanKassa.Backend.Api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("/api/employees")]
public class EmployeesController : BaseController<IEmployeesService>
{
    private readonly IEmployeeEditService employeeEditService;

    public EmployeesController(IEmployeesService employeesService, IEmployeeEditService employeeEditService) : base(
        employeesService)
    {
        this.employeeEditService = employeeEditService;
    }

    /// <summary>
    /// Get all employees list by page parameters
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Returns if employees exits</response>
    /// <response code="204">Returns if employees not found</response>
    [Route("all")]
    [HttpGet]
    [ProducesResponseType(typeof(PageEmployeesDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmployeesAsync([FromQuery] EmployeesPageParameters parameters)
        => Ok(
            await Service.GetEmployeesWithFiltersAsync(parameters)
        );

    /// <summary>
    /// Delete employees by sended Ids
    /// </summary>
    /// <param name="deletedIds"></param>
    /// <returns></returns>
    /// <response code="200">Returns if employees successfully deleted</response>
    /// <response code="400">Returns if errors occur while delete</response>
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [Route("delete")]
    [HttpPost]
    public async Task<IActionResult> DeleteEmployeesByIdAsync([FromBody] IEnumerable<int> deletedIds)
    {
        await Service.DeleteEmployeesAsync(deletedIds);
        return Ok();
    }

    /// <summary>
    /// Get employee for edit manipulations
    /// </summary>
    /// <param name="employeeId"></param>
    /// <returns></returns>
    /// <response code="200">Return if edited employee was found</response>
    /// <response code="404">Return if edited employee was not found</response>
    [Route("edit")]
    [HttpGet]
    [ProducesResponseType(typeof(EditedEmployeeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEditedEmployeeByIdAsync([FromQuery] int employeeId)
        => Ok(
            await employeeEditService.GetEditedEmployeeByIdAsync(employeeId)
        );

    /// <summary>
    /// Change sended employee
    /// </summary>
    /// <param name="changedEmp"></param>
    /// <returns></returns>
    /// <response code="200">Return if update was successfully canceled</response>
    /// <response code="400">Return if update failed</response>
    [Route("edit")]
    [HttpPatch]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangeEmployeeAsync([FromBody] ChangedEmployeeRequestDto changedEmp)
    {
        await employeeEditService.ChangeEmployeeAsync(changedEmp);
        return Ok();
    }

    /// <summary>
    /// Saved created employee
    /// </summary>
    /// <param name="savedEmployeeRequest"></param>
    /// <returns></returns>
    /// <response code="200">Return if update was successfully canceled</response>
    /// <response code="400">Return if create failed</response>
    [Route("edit/save")]
    [HttpPost]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SaveEmployeeAsync([FromBody] SavedEmployeeRequestDto savedEmployeeRequest)
    {
        await employeeEditService.SaveEmployeeAsync(savedEmployeeRequest);
        return Ok();
    }
}