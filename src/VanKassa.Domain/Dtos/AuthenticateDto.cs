namespace VanKassa.Domain.Dtos;

public class AuthenticateDto
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}