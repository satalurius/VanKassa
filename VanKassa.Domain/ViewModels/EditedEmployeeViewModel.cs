using System.ComponentModel.DataAnnotations;

namespace VanKassa.Domain.ViewModels;

public class EditedEmployeeViewModel
{
    public int UserId { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Не указана фамилия")]
    [MinLength(1, ErrorMessage = "Не указана фамилия")]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public string LastName { get; set; } = null!;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Не указано имя")]
    [MinLength(1, ErrorMessage = "Не указано имя")]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public string FirstName { get; set; } = null!;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Не указано отчество")]
    [StringLength(100, ErrorMessage = "Не указано отчество", MinimumLength = 1)]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public string Patronymic { get; set; } = null!;

    public string Photo { get; set; } = string.Empty;

    public EmployeeRoleViewModel Role { get; set; } = new();
    
    public IEnumerable<EmployeeOutletViewModel> Outlets { get; set; } = Array.Empty<EmployeeOutletViewModel>();
}