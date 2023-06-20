namespace VanKassa.Domain.Dtos.Admins
{
    public class AdministratorDto : AdminDtoBase
    {
        public int AdminId { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
