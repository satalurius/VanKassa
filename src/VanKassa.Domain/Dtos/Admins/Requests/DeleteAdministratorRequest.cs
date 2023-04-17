namespace VanKassa.Domain.Dtos.Admins.Requests
{
    public class DeleteAdministratorsRequest
    {
        public IReadOnlyList<int> DeletedIds { get; set; } = new List<int>();
    }
}
