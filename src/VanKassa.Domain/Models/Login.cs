using System.ComponentModel.DataAnnotations;

namespace VanKassa.Domain.Models;

public class Login
{
    [Required(AllowEmptyStrings = false)]
    [DisplayFormat(ConvertEmptyStringToNull=false)]
    public string UserName { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = false)]
    [DisplayFormat(ConvertEmptyStringToNull=false)]
    public string Password { get; set; } = string.Empty;
}