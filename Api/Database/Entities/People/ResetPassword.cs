using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Database.Entities.People
{
    [Table("ResetPassword", Schema = "People")]
    public class ResetPassword : BaseEntity
    {
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }

        public User? User { get; set; }
    }
}
