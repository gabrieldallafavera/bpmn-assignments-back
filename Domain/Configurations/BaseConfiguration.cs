using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public abstract class BaseConfiguration : IEntityTypeConfiguration<BaseEntity>
    {
        public void Configure(EntityTypeBuilder<BaseEntity> builder)
        {
            builder
                .Property(x => x.Created)
                .HasColumnType("datetime")
                .HasDefaultValue("getdate()")
                .IsRequired();

            builder
                .Property(x => x.Updated)
                .HasColumnType("datetime");

            builder
                .Property(x => x.Deleted)
                .HasColumnType("datetime");
        }
    }
}
