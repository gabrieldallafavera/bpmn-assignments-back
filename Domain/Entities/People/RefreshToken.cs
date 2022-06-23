using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.People
{
    [Table("User", Schema = "People")]
    public class RefreshToken : BaseEntity
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
    }
}
