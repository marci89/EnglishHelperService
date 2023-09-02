using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishHelperService.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Member"),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 9, 2, 16, 37, 48, 31, DateTimeKind.Utc).AddTicks(5131)),
                    LastActive = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 9, 2, 16, 37, 48, 31, DateTimeKind.Utc).AddTicks(5321))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "Role", "Username" },
                values: new object[] { 1L, "admin@example.com", new byte[] { 122, 228, 210, 115, 80, 255, 174, 216, 176, 54, 164, 214, 196, 236, 75, 140, 248, 191, 59, 104, 244, 143, 232, 30, 82, 69, 82, 12, 103, 54, 196, 175, 105, 96, 214, 28, 243, 58, 146, 75, 211, 37, 165, 134, 88, 56, 79, 51, 183, 214, 43, 237, 52, 128, 175, 222, 13, 0, 167, 157, 216, 20, 24, 89 }, new byte[] { 21, 102, 213, 180, 254, 141, 20, 184, 82, 176, 184, 80, 199, 255, 129, 34, 76, 247, 214, 82, 105, 99, 174, 87, 226, 221, 214, 24, 143, 220, 188, 74, 44, 9, 76, 146, 129, 218, 161, 39, 134, 35, 149, 216, 35, 120, 130, 69, 189, 188, 193, 5, 73, 164, 241, 89, 135, 230, 136, 56, 4, 235, 183, 208, 200, 191, 91, 73, 152, 197, 62, 126, 18, 188, 68, 230, 80, 193, 250, 142, 59, 61, 123, 25, 93, 54, 46, 26, 245, 134, 55, 75, 30, 218, 226, 158, 126, 7, 216, 10, 0, 130, 237, 51, 187, 141, 240, 223, 186, 238, 15, 98, 230, 129, 4, 154, 19, 102, 140, 134, 18, 223, 58, 59, 151, 23, 122, 188 }, "Admin", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
