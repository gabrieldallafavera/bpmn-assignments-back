using System.ComponentModel.DataAnnotations;

namespace Api.Database.Dtos.People
{
    public class RefreshTokenDto : BaseDto
    {
        public int UserId { get; set; }
        [Required]
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }

        public string NewToken { get; set; } = string.Empty;
    }
}
