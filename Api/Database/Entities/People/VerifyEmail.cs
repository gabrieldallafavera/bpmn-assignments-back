using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Database.Entities.People
{
    [Table("VerifyEmail", Schema = "People")]
    public class VerifyEmail : BaseEntity
    {
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }

        public User? User { get; set; }
    }
}
