﻿using Api.Database.Entities.People;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Database.Configurations.People
{
    internal class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            
            builder
                .Property(x => x.Name)
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder
                .HasIndex(x => x.Username)
                .IsUnique();
            
            builder
                .Property(x => x.Username)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .HasIndex(x => x.Email)
                .IsUnique();

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
