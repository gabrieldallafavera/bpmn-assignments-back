using Api.Database.Entities.People;

namespace Api.Database.Configurations.People
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            
            builder
                .Property(x => x.Name)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder
                .HasIndex(x => x.Username)
                .IsUnique();
            
            builder
                .Property(x => x.Username)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder
                .HasIndex(x => x.Email)
                .IsUnique();

            builder
                .Property(x => x.Email)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .Property(x => x.VerifiedAt)
                .HasColumnType("datetime");
        }
    }
}
