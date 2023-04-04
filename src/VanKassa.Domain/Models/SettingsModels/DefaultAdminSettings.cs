namespace VanKassa.Domain.Models.SettingsModels
{
    /// <summary>
    /// Модель настроек администратора по умолчанию.
    /// </summary>
    public class DefaultAdminSettings
    {
        public string Password { get; set; } = string.Empty;
    }
}
