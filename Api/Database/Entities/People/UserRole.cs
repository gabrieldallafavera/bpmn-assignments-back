using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Database.Entities.People
{
    [Table("UserRole", Schema = "People")]
    public class UserRole : BaseEntity
    {
        public int UserId { get; set; }
        public string Role { get; set; } = string.Empty;

        public User? User { get; set; }
    }
}
