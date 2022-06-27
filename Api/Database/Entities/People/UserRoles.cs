using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Database.Entities.People
{
    [Table("UserRoles", Schema = "People")]
    public class UserRoles : BaseEntity
    {
        public int UserId { get; set; }
        public string Role { get; set; }
    }
}
