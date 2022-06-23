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
                .Property(x => x.Email)
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder
                .Property(x => x.Password)
                .HasColumnType("varchar(250)")
                .IsRequired();
        }
    }
}
