// <auto-generated />
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

            modelBuilder.Entity("Api.Database.Entities.People.TokenFunction", b =>
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

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TokenFunction", "People");
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
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("datetime");

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
                            Created = new DateTime(2022, 6, 29, 10, 8, 21, 913, DateTimeKind.Local).AddTicks(7112),
                            Email = "myadminuser@email.com",
                            Name = "My Admin User",
                            PasswordHash = new byte[] { 126, 8, 105, 126, 46, 225, 221, 57, 181, 147, 12, 16, 78, 45, 216, 201, 226, 7, 91, 193, 230, 190, 30, 199, 117, 233, 68, 120, 21, 111, 37, 30, 127, 249, 70, 108, 141, 54, 135, 167, 145, 28, 242, 219, 239, 61, 19, 179, 61, 26, 96, 86, 168, 90, 131, 223, 110, 55, 76, 59, 58, 186, 106, 74 },
                            PasswordSalt = new byte[] { 245, 95, 187, 233, 70, 56, 240, 11, 213, 223, 144, 85, 185, 85, 235, 183, 234, 231, 121, 138, 95, 145, 183, 209, 3, 12, 139, 207, 4, 88, 123, 236, 61, 255, 82, 153, 253, 50, 28, 3, 97, 174, 18, 94, 125, 31, 118, 101, 84, 238, 201, 75, 113, 186, 147, 80, 158, 10, 200, 74, 90, 211, 236, 195, 15, 63, 107, 236, 96, 68, 89, 218, 53, 183, 220, 230, 124, 166, 116, 67, 183, 172, 244, 39, 23, 45, 232, 20, 175, 151, 50, 66, 156, 248, 7, 131, 79, 92, 83, 131, 151, 105, 237, 139, 110, 175, 214, 157, 36, 142, 59, 243, 69, 21, 39, 71, 80, 158, 148, 117, 108, 133, 221, 24, 200, 54, 80, 12 },
                            Username = "MyAdminUser",
                            VerifiedAt = new DateTime(2022, 6, 29, 10, 8, 21, 913, DateTimeKind.Local).AddTicks(7125)
                        });
                });

            modelBuilder.Entity("Api.Database.Entities.People.UserRole", b =>
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

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole", "People");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2022, 6, 29, 10, 8, 21, 913, DateTimeKind.Local).AddTicks(7227),
                            Role = "Admin",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2022, 6, 29, 10, 8, 21, 913, DateTimeKind.Local).AddTicks(7230),
                            Role = "User",
                            UserId = 1
                        });
                });

            modelBuilder.Entity("Api.Database.Entities.People.TokenFunction", b =>
                {
                    b.HasOne("Api.Database.Entities.People.User", "User")
                        .WithMany("TokenFunction")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Api.Database.Entities.People.UserRole", b =>
                {
                    b.HasOne("Api.Database.Entities.People.User", "User")
                        .WithMany("UserRole")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Api.Database.Entities.People.User", b =>
                {
                    b.Navigation("TokenFunction");

                    b.Navigation("UserRole");
                });
#pragma warning restore 612, 618
        }
    }
}
