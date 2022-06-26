﻿// <auto-generated />
using System;
using Api.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.Database.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Api.Database.Entities.People.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Token")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshToken", "People");
                });

            modelBuilder.Entity("Api.Database.Entities.People.ResetPassword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Token")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ResetPassword", "People");
                });

            modelBuilder.Entity("Api.Database.Entities.People.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("User", "People");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2022, 6, 26, 18, 51, 51, 677, DateTimeKind.Local).AddTicks(4365),
                            Email = "myadminuser@email.com",
                            Name = "My Admin User",
                            PasswordHash = new byte[] { 65, 179, 76, 151, 76, 73, 125, 59, 64, 62, 219, 154, 177, 121, 113, 107, 138, 135, 192, 38, 213, 130, 185, 243, 62, 209, 67, 41, 164, 186, 239, 44, 89, 47, 206, 145, 146, 252, 224, 92, 160, 145, 226, 8, 210, 184, 43, 8, 198, 107, 51, 159, 71, 218, 183, 45, 239, 147, 137, 132, 229, 83, 116, 123 },
                            PasswordSalt = new byte[] { 46, 193, 121, 145, 166, 110, 87, 224, 9, 104, 96, 194, 88, 39, 162, 91, 214, 191, 71, 86, 148, 13, 108, 100, 118, 31, 195, 228, 86, 196, 193, 168, 12, 177, 220, 33, 175, 166, 190, 248, 14, 14, 166, 210, 215, 216, 41, 249, 210, 14, 231, 84, 142, 85, 83, 219, 164, 243, 156, 143, 231, 41, 17, 132, 43, 67, 67, 37, 93, 112, 235, 167, 162, 178, 1, 235, 221, 201, 8, 186, 30, 122, 188, 22, 155, 70, 197, 230, 96, 218, 245, 133, 174, 141, 55, 177, 154, 211, 193, 45, 249, 239, 42, 193, 52, 50, 211, 202, 124, 179, 252, 143, 96, 172, 5, 123, 135, 54, 187, 210, 146, 98, 157, 248, 46, 99, 27, 237 },
                            Role = "Admin",
                            Username = "MyAdminUser"
                        });
                });

            modelBuilder.Entity("Api.Database.Entities.People.VerifyEmail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Token")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("VerifyEmail", "People");
                });

            modelBuilder.Entity("Api.Database.Entities.People.RefreshToken", b =>
                {
                    b.HasOne("Api.Database.Entities.People.User", "User")
                        .WithOne("RefreshToken")
                        .HasForeignKey("Api.Database.Entities.People.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Api.Database.Entities.People.ResetPassword", b =>
                {
                    b.HasOne("Api.Database.Entities.People.User", "User")
                        .WithOne("ResetPassword")
                        .HasForeignKey("Api.Database.Entities.People.ResetPassword", "UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Api.Database.Entities.People.VerifyEmail", b =>
                {
                    b.HasOne("Api.Database.Entities.People.User", "User")
                        .WithOne("VerifyEmail")
                        .HasForeignKey("Api.Database.Entities.People.VerifyEmail", "UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Api.Database.Entities.People.User", b =>
                {
                    b.Navigation("RefreshToken");

                    b.Navigation("ResetPassword");

                    b.Navigation("VerifyEmail");
                });
#pragma warning restore 612, 618
        }
    }
}
