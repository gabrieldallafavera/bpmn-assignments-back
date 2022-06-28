using Api.Database.Entities.People;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Database.Configurations.People
{
    internal class UserRolesConfiguration : BaseEntityConfiguration<UserRoles>
    {
        public override void Configure(EntityTypeBuilder<UserRoles> builder)
        {
            base.Configure(builder);

            builder
                .HasOne(x => x.User)
                .WithMany(c => c.UserRoles)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(x => x.Role)
                .HasColumnType("varchar(50)")
                .IsRequired();
        }
    }
}
