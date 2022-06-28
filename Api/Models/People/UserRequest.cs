using System.ComponentModel.DataAnnotations;

namespace Api.Models.People
{
    public class UserRequest : BaseModel
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
        public IList<UserRoleRequest>? UserRoleRequest { get; set; }
    }
}
