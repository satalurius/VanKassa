namespace VanKassa.Domain.Dtos.Admins.Requests
{
    public class CreateAdministratorRequest : AdminDtoBase
    {
        public string Password { get; set; } = string.Empty;
    }
}
