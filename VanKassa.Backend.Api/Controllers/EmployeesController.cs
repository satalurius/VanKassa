using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Domain.Dtos;
using VanKassa.Domain.ViewModels;

namespace VanKassa.Backend.Api.Controllers;


[ApiController]
[Route("/api/employees")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeesService employeesService;
    private readonly IEmployeeEditService employeeEditService;

    public EmployeesController(IEmployeesService employeesService, IEmployeeEditService employeeEditService)
    {
        this.employeesService = employeesService;
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
    [ProducesResponseType(typeof(PageEmployeesDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmployeesAsync([FromQuery] EmployeesPageParameters parameters)
    {
        try
        {
            var empDto = await employeesService.GetEmployeesWithFiltersAsync(parameters);

            if (empDto is null)
                return NotFound(Array.Empty<PageEmployeesDto>());

            return Ok(empDto);
        }
        catch (OperationCanceledException)
        {
            return NotFound(Array.Empty<PageEmployeesDto>());
        }

    }

    /// <summary>
    /// Delete employees by sended Ids
    /// </summary>
    /// <param name="deletedIds"></param>
    /// <returns></returns>
    /// <response code="200">Returns if employees successfully deleted</response>
    /// <response code="400">Returns if errors occur while delete</response>
    [Route("delete")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteEmployeesByIdAsync([FromBody] IEnumerable<int> deletedIds)
    {
        try
        {
            await employeesService.DeleteEmployeesAsync(deletedIds);
            return Ok();
        }
        catch (OperationCanceledException)
        {
            return BadRequest();
        }
        catch (ArgumentNullException)
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// Get employee for edit manipulations
    /// </summary>
    /// <param name="employeeId"></param>
    /// <returns></returns>
    /// <response code="200">Return if edited employee was found</response>
    /// <response code="404">Return if edited employee was not found</response>
    [Route("edit/get_employee")]
    [HttpGet]
    [ProducesResponseType(typeof(EditedEmployeeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EditedEmployeeDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEditedEmployeeByIdAsync([FromQuery] int employeeId)
    {
        var emp = await employeeEditService.GetEditedEmployeeByIdAsync(employeeId);

        if (emp is null)
            return NotFound(new EditedEmployeeDto());

        return Ok(emp);
    }

    /// <summary>
    /// Change sended employee
    /// </summary>
    /// <param name="edited"></param>
    /// <returns></returns>
    /// <response code="200">Return if update was successfully canceled</response>
    /// <response code="400">Return if update failed</response>
    [Route("edit/change_employee")]
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangeEmployeeAsync([FromBody] EditedEmployeeDto edited)
    {
        try
        {
            await employeeEditService.ChangeExistEmployeeAsync(edited);
            return Ok();
        }
        catch (InvalidOperationException)
        {
            return BadRequest("Update was failed");
        }
        catch (ArgumentNullException)
        {
            return BadRequest("Update was failed");
        }
    }

    [Route("edit/save_employee")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SaveEmployeeAsync([FromBody] EditedEmployeeDto savedEmployee)
    {
        try
        {
            await employeeEditService.SaveEmployeeAsync(savedEmployee);

            return Ok();
        }
        catch (InvalidOperationException)
        {
            return BadRequest("Save was failed");
        }
    }

}