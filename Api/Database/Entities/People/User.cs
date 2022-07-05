namespace Api.Database.Entities.People
{
    [Table("User", Schema = "People")]
    public class User : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public DateTime? VerifiedAt { get; set; }

        public IList<UserRole>? UserRole { get; set; }
        public IList<TokenFunction>? TokenFunction { get; set; }
    }
}
