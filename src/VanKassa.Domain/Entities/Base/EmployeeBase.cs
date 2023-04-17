namespace VanKassa.Domain.Entities.Base
{
    public abstract class EmployeeBase
    {
        public int UserId { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
    }
}
