using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using VanKassa.Backend.Api.Controllers.Base;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Domain.Constants;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Dtos.Employees.Requests;

namespace VanKassa.Backend.Api.Controllers;

[EnableCors(PolicyConstants.WebPolicy)]
[ApiController]
[Route("/api/employees")]
public class EmployeesController : BaseController<IEmployeesService>
{
    private readonly IEmployeeEditService employeeEditService;

    public EmployeesController(IEmployeesService employeesService, IEmployeeEditService employeeEditService) : base(employeesService)
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
    [ProducesResponseType(typeof(PageEmployeesDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmployeesAsync([FromQuery] EmployeesPageParameters parameters)
    {
        try
        {
            var empDto = await Service.GetEmployeesWithFiltersAsync(parameters);

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
    public async Task<IActionResult> DeleteEmployeesByIdAsync([FromBody] IEnumerable<int> deletedIds)
    {
        try
        {
            await Service.DeleteEmployeesAsync(deletedIds);
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
    [Route("edit")]
    [HttpGet]
    [ProducesResponseType(typeof(EditedEmployeeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EditedEmployeeDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEditedEmployeeByIdAsync([FromQuery] int employeeId)
    {
        try
        {
             return Ok(
                 await employeeEditService.GetEditedEmployeeByIdAsync(employeeId)
             );
        }
        catch (InvalidOperationException)
        {
            return NotFound(new EditedEmployeeDto());
        }
    }

    /// <summary>
    /// Change sended employee
    /// </summary>
    /// <param name="changedEmp"></param>
    /// <returns></returns>
    /// <response code="200">Return if update was successfully canceled</response>
    /// <response code="400">Return if update failed</response>
    [Route("edit")]
    [HttpPut]
    public async Task<IActionResult> ChangeEmployeeAsync([FromBody] ChangedEmployeeRequestDto changedEmp)
    {
        try
        {
            await employeeEditService.ChangeEmployeeAsync(changedEmp);
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
    /// <summary>
    /// Saved created employee
    /// </summary>
    /// <param name="savedEmployeeRequest"></param>
    /// <returns></returns>
    /// <response code="200">Return if update was successfully canceled</response>
    /// <response code="400">Return if create failed</response>
    [Route("edit/save")]
    [HttpPost]
    public async Task<IActionResult> SaveEmployeeAsync([FromBody] SavedEmployeeRequestDto savedEmployeeRequest)
    {
        try
        {
            await employeeEditService.SaveEmployeeAsync(savedEmployeeRequest);
            return Ok();
        }
        catch (InvalidOperationException)
        {
            return BadRequest("Save was failed");
        }
    }

}