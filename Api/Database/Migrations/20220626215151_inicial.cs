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
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "ResetPassword",
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
                    table.PrimaryKey("PK_ResetPassword", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResetPassword_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "People",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VerifyEmail",
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
                    table.PrimaryKey("PK_VerifyEmail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerifyEmail_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "People",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "People",
                table: "User",
                columns: new[] { "Id", "Created", "Deleted", "Email", "Name", "PasswordHash", "PasswordSalt", "Role", "Updated", "Username", "VerifiedAt" },
                values: new object[] { 1, new DateTime(2022, 6, 26, 18, 51, 51, 677, DateTimeKind.Local).AddTicks(4365), null, "myadminuser@email.com", "My Admin User", new byte[] { 65, 179, 76, 151, 76, 73, 125, 59, 64, 62, 219, 154, 177, 121, 113, 107, 138, 135, 192, 38, 213, 130, 185, 243, 62, 209, 67, 41, 164, 186, 239, 44, 89, 47, 206, 145, 146, 252, 224, 92, 160, 145, 226, 8, 210, 184, 43, 8, 198, 107, 51, 159, 71, 218, 183, 45, 239, 147, 137, 132, 229, 83, 116, 123 }, new byte[] { 46, 193, 121, 145, 166, 110, 87, 224, 9, 104, 96, 194, 88, 39, 162, 91, 214, 191, 71, 86, 148, 13, 108, 100, 118, 31, 195, 228, 86, 196, 193, 168, 12, 177, 220, 33, 175, 166, 190, 248, 14, 14, 166, 210, 215, 216, 41, 249, 210, 14, 231, 84, 142, 85, 83, 219, 164, 243, 156, 143, 231, 41, 17, 132, 43, 67, 67, 37, 93, 112, 235, 167, 162, 178, 1, 235, 221, 201, 8, 186, 30, 122, 188, 22, 155, 70, 197, 230, 96, 218, 245, 133, 174, 141, 55, 177, 154, 211, 193, 45, 249, 239, 42, 193, 52, 50, 211, 202, 124, 179, 252, 143, 96, 172, 5, 123, 135, 54, 187, 210, 146, 98, 157, 248, 46, 99, 27, 237 }, "Admin", null, "MyAdminUser", null });

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
                name: "IX_ResetPassword_Token",
                schema: "People",
                table: "ResetPassword",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResetPassword_UserId",
                schema: "People",
                table: "ResetPassword",
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

            migrationBuilder.CreateIndex(
                name: "IX_VerifyEmail_Token",
                schema: "People",
                table: "VerifyEmail",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VerifyEmail_UserId",
                schema: "People",
                table: "VerifyEmail",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "People");

            migrationBuilder.DropTable(
                name: "ResetPassword",
                schema: "People");

            migrationBuilder.DropTable(
                name: "VerifyEmail",
                schema: "People");

            migrationBuilder.DropTable(
                name: "User",
                schema: "People");
        }
    }
}
