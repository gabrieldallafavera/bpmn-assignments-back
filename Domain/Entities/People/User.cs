using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.People
{
    [Table("User", Schema = "People")]
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
