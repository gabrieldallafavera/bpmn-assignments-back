using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Database.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "People");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(250)", nullable: false),
                    Username = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(250)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Role = table.Column<string>(type: "varchar(50)", nullable: false),
                    RefreshToken = table.Column<string>(type: "varchar(200)", nullable: false),
                    TokenCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    TokenExpires = table.Column<DateTime>(type: "datetime", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "People",
                table: "User",
                columns: new[] { "Id", "Created", "Deleted", "Email", "Name", "PasswordHash", "PasswordSalt", "RefreshToken", "Role", "TokenCreated", "TokenExpires", "Updated", "Username" },
                values: new object[] { 1, new DateTime(2022, 6, 23, 16, 12, 3, 740, DateTimeKind.Local).AddTicks(6632), null, "myadminuser@email.com", "My Admin User", new byte[] { 169, 123, 157, 160, 130, 71, 130, 24, 62, 177, 64, 236, 186, 202, 215, 101, 246, 130, 112, 113, 222, 172, 30, 228, 114, 209, 33, 244, 71, 61, 36, 147, 219, 198, 196, 156, 217, 236, 73, 8, 134, 154, 113, 51, 59, 161, 29, 194, 177, 48, 72, 118, 0, 241, 16, 145, 154, 120, 9, 183, 68, 133, 169, 223 }, new byte[] { 254, 180, 151, 14, 97, 28, 100, 179, 221, 42, 250, 208, 214, 46, 171, 157, 248, 8, 96, 235, 118, 153, 164, 225, 90, 67, 41, 164, 247, 194, 248, 67, 6, 63, 93, 203, 98, 87, 139, 145, 237, 176, 204, 170, 8, 145, 242, 61, 77, 75, 11, 251, 222, 150, 201, 72, 125, 250, 67, 180, 98, 117, 125, 27, 212, 252, 2, 62, 178, 144, 121, 130, 172, 100, 100, 18, 183, 5, 154, 15, 192, 207, 184, 145, 40, 4, 16, 0, 99, 40, 53, 31, 90, 15, 177, 67, 206, 230, 127, 223, 160, 99, 167, 45, 24, 171, 18, 74, 214, 229, 185, 23, 150, 142, 138, 41, 108, 37, 68, 245, 55, 8, 191, 154, 242, 161, 188, 224 }, "", "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MyAdminUser" });

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                schema: "People",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                schema: "People",
                table: "User",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User",
                schema: "People");
        }
    }
}
