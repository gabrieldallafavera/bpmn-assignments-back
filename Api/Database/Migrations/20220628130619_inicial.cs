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
                values: new object[] { 1, new DateTime(2022, 6, 28, 10, 6, 19, 729, DateTimeKind.Local).AddTicks(1797), null, "myadminuser@email.com", "My Admin User", new byte[] { 14, 83, 84, 233, 156, 115, 68, 150, 44, 37, 248, 57, 187, 45, 123, 72, 169, 177, 156, 247, 170, 49, 208, 107, 210, 157, 215, 67, 186, 35, 94, 64, 77, 140, 58, 233, 80, 7, 251, 45, 47, 106, 88, 115, 213, 237, 33, 188, 206, 58, 181, 54, 19, 9, 171, 254, 205, 62, 54, 162, 147, 105, 120, 112 }, new byte[] { 245, 30, 239, 62, 102, 19, 232, 143, 234, 129, 253, 94, 106, 254, 160, 76, 182, 75, 224, 129, 85, 128, 21, 12, 164, 208, 61, 146, 51, 152, 221, 136, 201, 60, 231, 87, 172, 49, 188, 39, 118, 255, 221, 54, 23, 213, 107, 23, 249, 123, 56, 225, 215, 146, 77, 21, 187, 45, 219, 232, 12, 193, 133, 229, 204, 87, 52, 123, 198, 11, 56, 18, 2, 79, 86, 39, 113, 45, 225, 46, 106, 14, 210, 224, 1, 97, 145, 143, 175, 20, 133, 104, 143, 143, 212, 54, 64, 143, 119, 137, 28, 42, 51, 41, 211, 23, 146, 203, 190, 50, 32, 20, 137, 250, 169, 20, 27, 228, 20, 102, 243, 102, 242, 207, 250, 173, 123, 82 }, null, "MyAdminUser", new DateTime(2022, 6, 28, 10, 6, 19, 729, DateTimeKind.Local).AddTicks(1811) });

            migrationBuilder.InsertData(
                schema: "People",
                table: "UserRole",
                columns: new[] { "Id", "Created", "Deleted", "Role", "Updated", "UserId" },
                values: new object[] { 1, new DateTime(2022, 6, 28, 10, 6, 19, 729, DateTimeKind.Local).AddTicks(1930), null, "Admin", null, 1 });

            migrationBuilder.InsertData(
                schema: "People",
                table: "UserRole",
                columns: new[] { "Id", "Created", "Deleted", "Role", "Updated", "UserId" },
                values: new object[] { 2, new DateTime(2022, 6, 28, 10, 6, 19, 729, DateTimeKind.Local).AddTicks(1932), null, "User", null, 1 });

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
