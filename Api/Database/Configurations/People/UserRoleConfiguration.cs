using Api.Database.Entities.People;

namespace Api.Database.Configurations.People
{
    public class UserRoleConfiguration : BaseEntityConfiguration<UserRole>
    {
        public override void Configure(EntityTypeBuilder<UserRole> builder)
        {
            base.Configure(builder);

            builder
                .Property(x => x.Role)
                .HasColumnType("varchar(50)")
                .IsRequired();
            
            builder
                .HasOne(x => x.User)
                .WithMany(c => c.UserRole)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
