using System.ComponentModel.DataAnnotations;

namespace VanKassa.Domain.ViewModels;

public class EmployeeOutletViewModel
{
    [Required]
    public int Id { get; set; }    
    [Required]
    public string Address { get; set; } = string.Empty;
}