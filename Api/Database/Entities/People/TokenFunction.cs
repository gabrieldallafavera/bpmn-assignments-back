using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Database.Entities.People
{
    [Table("TokenFunction", Schema = "People")]
    public class TokenFunction : BaseEntity
    {
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public int Type { get; set; }
        public DateTime Expires { get; set; }

        public User? User { get; set; }
    }
}
