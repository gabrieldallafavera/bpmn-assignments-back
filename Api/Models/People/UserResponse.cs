using System.ComponentModel.DataAnnotations;

namespace Api.Models.People
{
    public class UserResponse : BaseModel
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; } = string.Empty;

        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpires { get; set; }
    }
}
