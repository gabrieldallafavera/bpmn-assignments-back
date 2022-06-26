using Api.Database.Entities.People;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Database.Configurations.People
{
    public class VerifyEmailConfiguration : BaseEntityConfiguration<VerifyEmail>
    {
        public override void Configure(EntityTypeBuilder<VerifyEmail> builder)
        {
            base.Configure(builder);

            builder
                .HasOne(l => l.User)
                .WithOne(c => c.VerifyEmail)
                .HasForeignKey<VerifyEmail>(l => l.UserId)
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
