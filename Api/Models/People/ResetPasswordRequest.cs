namespace Api.Models.People
{
    public class ResetPasswordRequest
    {
        [Required, RegularExpression(@"^(?=.*[A-Z])(?=.*[!#@$%&])(?=.*[0-9])(?=.*[a-z]).{8,}$", ErrorMessage = "A senha não atende os padrões.")]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password", ErrorMessage = "As senhas não batem.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
