namespace VanKassa.Domain.ViewModels
{
    /// <summary>
    /// Модель администратора в системе.
    /// </summary>
    public class AdministratorViewModel
    {
        public int AdminId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsNew { get; set; }
    }
}