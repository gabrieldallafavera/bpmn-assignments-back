using Api.Database.Entities.People;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Database.Configurations.People
{
    public class ResetPasswordConfiguration : BaseEntityConfiguration<ResetPassword>
    {
        public override void Configure(EntityTypeBuilder<ResetPassword> builder)
        {
            base.Configure(builder);

            builder
                .HasOne(l => l.User)
                .WithOne(c => c.ResetPassword)
                .HasForeignKey<ResetPassword>(l => l.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasIndex(x => x.Token)
                .IsUnique();

            builder
                .Property(x => x.Token)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder
                .Property(x => x.Expires)
                .HasColumnType("datetime")
                .IsRequired();
        }
    }
}
