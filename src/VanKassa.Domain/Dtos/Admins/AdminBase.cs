namespace VanKassa.Domain.Dtos.Admins
{
    public abstract class AdminDtoBase
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
