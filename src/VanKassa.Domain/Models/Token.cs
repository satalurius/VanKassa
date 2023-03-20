namespace VanKassa.Domain.Models
{
    public class Token
    {
        public string JwtToken {get; private set; }
        public string RefreshToken {get; private set;}

        public Token(string jwtToken, string refreshToken)
        {
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}