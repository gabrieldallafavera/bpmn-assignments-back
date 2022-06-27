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
                values: new object[] { 1, new DateTime(2022, 6, 27, 9, 52, 5, 751, DateTimeKind.Local).AddTicks(8817), null, "myadminuser@email.com", "My Admin User", new byte[] { 242, 119, 195, 202, 84, 46, 17, 128, 240, 98, 22, 50, 21, 187, 193, 198, 148, 47, 239, 212, 102, 42, 40, 29, 7, 59, 136, 43, 35, 138, 206, 144, 187, 54, 60, 166, 243, 56, 253, 108, 95, 184, 29, 59, 52, 101, 138, 232, 11, 47, 1, 10, 26, 115, 55, 46, 5, 9, 117, 254, 213, 126, 37, 128 }, new byte[] { 198, 169, 145, 149, 247, 173, 16, 11, 142, 132, 57, 242, 36, 62, 7, 151, 159, 169, 152, 235, 116, 227, 66, 99, 33, 37, 219, 99, 138, 131, 22, 24, 9, 5, 3, 251, 3, 77, 207, 53, 187, 146, 111, 215, 148, 116, 51, 43, 244, 24, 162, 71, 161, 149, 102, 140, 171, 197, 41, 140, 140, 91, 156, 14, 156, 58, 130, 212, 41, 45, 88, 86, 234, 208, 212, 144, 140, 167, 172, 149, 41, 83, 28, 110, 26, 1, 29, 121, 221, 248, 214, 141, 62, 19, 235, 170, 68, 195, 78, 227, 77, 252, 246, 227, 135, 44, 112, 20, 89, 138, 22, 65, 220, 56, 136, 114, 57, 61, 196, 215, 238, 145, 230, 240, 72, 53, 74, 233 }, "Admin", null, "MyAdminUser", new DateTime(2022, 6, 27, 9, 52, 5, 751, DateTimeKind.Local).AddTicks(8834) });

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
