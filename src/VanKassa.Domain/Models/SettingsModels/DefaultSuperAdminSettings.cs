namespace VanKassa.Domain.Models.SettingsModels;

/// <summary>
/// Модель настроек супер пользователя, создающегося по умолчанию.
/// </summary>
public class DefaultSuperAdminSettings
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}