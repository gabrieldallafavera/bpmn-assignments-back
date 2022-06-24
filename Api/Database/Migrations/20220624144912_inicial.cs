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
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "varchar(200)", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "People",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "People",
                table: "User",
                columns: new[] { "Id", "Created", "Deleted", "Email", "Name", "PasswordHash", "PasswordSalt", "Role", "Updated", "Username" },
                values: new object[] { 1, new DateTime(2022, 6, 24, 11, 49, 11, 977, DateTimeKind.Local).AddTicks(5391), null, "myadminuser@email.com", "My Admin User", new byte[] { 107, 183, 179, 146, 65, 157, 246, 196, 32, 57, 215, 212, 223, 249, 49, 71, 59, 245, 82, 166, 121, 139, 155, 165, 155, 98, 187, 94, 26, 156, 158, 192, 18, 134, 201, 75, 160, 101, 215, 170, 0, 254, 139, 21, 249, 240, 67, 143, 253, 209, 15, 95, 169, 46, 139, 12, 133, 170, 226, 180, 175, 200, 41, 18 }, new byte[] { 135, 86, 250, 178, 189, 86, 174, 214, 114, 109, 170, 220, 215, 242, 90, 32, 222, 60, 75, 145, 164, 68, 133, 8, 127, 8, 9, 164, 160, 242, 217, 71, 186, 8, 134, 96, 43, 61, 125, 243, 201, 49, 83, 33, 89, 116, 45, 192, 207, 146, 9, 159, 246, 35, 116, 90, 211, 58, 101, 238, 204, 152, 46, 125, 202, 61, 159, 140, 202, 131, 28, 227, 137, 254, 57, 113, 42, 128, 232, 176, 181, 147, 227, 78, 99, 227, 199, 63, 21, 142, 41, 13, 127, 211, 219, 76, 135, 163, 148, 212, 201, 196, 140, 140, 236, 46, 73, 193, 113, 63, 169, 237, 77, 88, 114, 144, 138, 135, 140, 231, 160, 200, 43, 224, 246, 194, 226, 163 }, "Admin", null, "MyAdminUser" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_Token",
                schema: "People",
                table: "RefreshToken",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                schema: "People",
                table: "RefreshToken",
                column: "UserId",
                unique: true);

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
                name: "RefreshToken",
                schema: "People");

            migrationBuilder.DropTable(
                name: "User",
                schema: "People");
        }
    }
}
