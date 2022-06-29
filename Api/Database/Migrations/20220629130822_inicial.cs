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
                    Name = table.Column<string>(type: "varchar(150)", nullable: false),
                    Username = table.Column<string>(type: "varchar(50)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    VerifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TokenFunction",
                schema: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "varchar(200)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenFunction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokenFunction_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "People",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "varchar(50)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "People",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "People",
                table: "User",
                columns: new[] { "Id", "Created", "Deleted", "Email", "Name", "PasswordHash", "PasswordSalt", "Updated", "Username", "VerifiedAt" },
                values: new object[] { 1, new DateTime(2022, 6, 29, 10, 8, 21, 913, DateTimeKind.Local).AddTicks(7112), null, "myadminuser@email.com", "My Admin User", new byte[] { 126, 8, 105, 126, 46, 225, 221, 57, 181, 147, 12, 16, 78, 45, 216, 201, 226, 7, 91, 193, 230, 190, 30, 199, 117, 233, 68, 120, 21, 111, 37, 30, 127, 249, 70, 108, 141, 54, 135, 167, 145, 28, 242, 219, 239, 61, 19, 179, 61, 26, 96, 86, 168, 90, 131, 223, 110, 55, 76, 59, 58, 186, 106, 74 }, new byte[] { 245, 95, 187, 233, 70, 56, 240, 11, 213, 223, 144, 85, 185, 85, 235, 183, 234, 231, 121, 138, 95, 145, 183, 209, 3, 12, 139, 207, 4, 88, 123, 236, 61, 255, 82, 153, 253, 50, 28, 3, 97, 174, 18, 94, 125, 31, 118, 101, 84, 238, 201, 75, 113, 186, 147, 80, 158, 10, 200, 74, 90, 211, 236, 195, 15, 63, 107, 236, 96, 68, 89, 218, 53, 183, 220, 230, 124, 166, 116, 67, 183, 172, 244, 39, 23, 45, 232, 20, 175, 151, 50, 66, 156, 248, 7, 131, 79, 92, 83, 131, 151, 105, 237, 139, 110, 175, 214, 157, 36, 142, 59, 243, 69, 21, 39, 71, 80, 158, 148, 117, 108, 133, 221, 24, 200, 54, 80, 12 }, null, "MyAdminUser", new DateTime(2022, 6, 29, 10, 8, 21, 913, DateTimeKind.Local).AddTicks(7125) });

            migrationBuilder.InsertData(
                schema: "People",
                table: "UserRole",
                columns: new[] { "Id", "Created", "Deleted", "Role", "Updated", "UserId" },
                values: new object[] { 1, new DateTime(2022, 6, 29, 10, 8, 21, 913, DateTimeKind.Local).AddTicks(7227), null, "Admin", null, 1 });

            migrationBuilder.InsertData(
                schema: "People",
                table: "UserRole",
                columns: new[] { "Id", "Created", "Deleted", "Role", "Updated", "UserId" },
                values: new object[] { 2, new DateTime(2022, 6, 29, 10, 8, 21, 913, DateTimeKind.Local).AddTicks(7230), null, "User", null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_TokenFunction_UserId",
                schema: "People",
                table: "TokenFunction",
                column: "UserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                schema: "People",
                table: "UserRole",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TokenFunction",
                schema: "People");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "People");

            migrationBuilder.DropTable(
                name: "User",
                schema: "People");
        }
    }
}
