namespace VanKassa.Backend.Infrastructure.IdentityEntities;

public class RefreshToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiredDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? RevokedDate { get; set; }
    public string ReplacedByToken { get; set; } = string.Empty;
    public string RevokedReason { get; set; } = string.Empty;
    public LoginUser LoginUser { get; set; } = new();
}