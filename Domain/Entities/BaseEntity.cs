namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
