using System.ComponentModel.DataAnnotations;

namespace Api.Database.Dtos.People
{
    public class UserReadDto : BaseDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; } = string.Empty;

        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpires { get; set; }
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordExpires { get; set; }
        public string? VerifyEmailToken { get; set; }
        public DateTime? VerifyEmailExpires { get; set; }
    }
}
