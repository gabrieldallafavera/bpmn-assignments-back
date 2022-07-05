namespace Api.Models.People
{
    public class RefreshTokenResponse : BaseModel
    {
        public int UserId { get; set; }
        [Required]
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }

        public string NewToken { get; set; } = string.Empty;
    }
}
