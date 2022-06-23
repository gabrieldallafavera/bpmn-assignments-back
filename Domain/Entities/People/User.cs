using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.People
{
    [Table("User", Schema = "People")]
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
