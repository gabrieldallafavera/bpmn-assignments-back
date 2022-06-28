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
                name: "UserRoles",
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
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_User_UserId",
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
                columns: new[] { "Id", "Created", "Deleted", "Email", "Name", "PasswordHash", "PasswordSalt", "Updated", "Username", "VerifiedAt" },
                values: new object[] { 1, new DateTime(2022, 6, 27, 20, 17, 14, 870, DateTimeKind.Local).AddTicks(6053), null, "myadminuser@email.com", "My Admin User", new byte[] { 166, 120, 63, 191, 102, 163, 209, 4, 177, 52, 39, 192, 207, 69, 203, 253, 91, 181, 158, 43, 76, 74, 130, 133, 132, 93, 70, 135, 93, 161, 87, 43, 228, 180, 10, 167, 109, 212, 131, 152, 14, 38, 155, 35, 247, 120, 252, 130, 71, 29, 97, 65, 170, 19, 83, 23, 64, 196, 199, 38, 33, 179, 214, 85 }, new byte[] { 12, 204, 72, 205, 114, 13, 109, 122, 159, 55, 153, 128, 78, 112, 237, 18, 245, 153, 95, 205, 92, 67, 129, 16, 138, 69, 63, 75, 196, 135, 227, 165, 242, 121, 246, 177, 32, 205, 93, 187, 126, 246, 48, 21, 244, 70, 179, 216, 65, 133, 182, 97, 144, 241, 0, 102, 160, 56, 28, 125, 76, 40, 230, 39, 50, 81, 165, 115, 140, 189, 243, 59, 173, 150, 221, 250, 247, 237, 100, 81, 81, 140, 215, 152, 191, 31, 116, 170, 95, 179, 127, 28, 34, 131, 186, 164, 216, 175, 64, 27, 83, 158, 111, 10, 132, 86, 163, 147, 56, 231, 32, 19, 179, 73, 130, 110, 33, 98, 253, 211, 240, 206, 34, 85, 132, 65, 179, 137 }, null, "MyAdminUser", new DateTime(2022, 6, 27, 20, 17, 14, 870, DateTimeKind.Local).AddTicks(6067) });

            migrationBuilder.InsertData(
                schema: "People",
                table: "UserRoles",
                columns: new[] { "Id", "Created", "Deleted", "Role", "Updated", "UserId" },
                values: new object[] { 1, new DateTime(2022, 6, 27, 20, 17, 14, 870, DateTimeKind.Local).AddTicks(6190), null, "Admin", null, 1 });

            migrationBuilder.InsertData(
                schema: "People",
                table: "UserRoles",
                columns: new[] { "Id", "Created", "Deleted", "Role", "Updated", "UserId" },
                values: new object[] { 2, new DateTime(2022, 6, 27, 20, 17, 14, 870, DateTimeKind.Local).AddTicks(6193), null, "User", null, 1 });

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
                name: "IX_UserRoles_UserId",
                schema: "People",
                table: "UserRoles",
                column: "UserId");

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
                name: "UserRoles",
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
