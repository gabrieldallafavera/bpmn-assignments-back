using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Database.Dtos
{
    public class BaseDto
    {
        public int? Id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
