using System.ComponentModel.DataAnnotations;

namespace Api.Database.Dtos.People
{
    public class UserWriteDto : BaseDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, RegularExpression(@"^(?=.*[A-Z])(?=.*[!#@$%&])(?=.*[0-9])(?=.*[a-z]).{8,}$", ErrorMessage = "A senha não atende os padrões.")]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password", ErrorMessage = "As senhas não batem.")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
