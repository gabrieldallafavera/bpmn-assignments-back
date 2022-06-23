using Domain.Entities.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations.People
{
    internal class UserConfiguration : BaseConfiguration, IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(x => x.Name)
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder
                .Property(x => x.Username)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .Property(x => x.Email)
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder
                .Property(x => x.Role)
                .HasColumnType("varchar(50)")
                .IsRequired();
        }
    }
}
