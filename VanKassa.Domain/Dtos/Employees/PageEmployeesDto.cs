namespace VanKassa.Domain.Dtos.Employees;

public class PageEmployeesDto
{
    /// <summary>
    /// Количество сотрудников.
    /// </summary>
    public int TotalCount { get; set; }

    public IEnumerable<EmployeesDbDto> EmployeesDbDtos { get; set; } = Array.Empty<EmployeesDbDto>();
}