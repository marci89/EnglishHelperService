using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false, defaultValue: "Member"),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastActive = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ResetToken = table.Column<string>(type: "text", nullable: true),
                    ResetTokenExpiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LearnStatistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CorrectCount = table.Column<int>(type: "integer", nullable: false),
                    IncorrectCount = table.Column<int>(type: "integer", nullable: false),
                    Result = table.Column<int>(type: "integer", nullable: false),
                    LearnMode = table.Column<string>(type: "text", nullable: false, defaultValue: "Flashcard"),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearnStatistics_UserID",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Word",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    EnglishText = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    HungarianText = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CorrectCount = table.Column<int>(type: "integer", nullable: false),
                    IncorrectCount = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUse = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Word", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Word_UserID",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Created", "Email", "LastActive", "Password", "ResetToken", "ResetTokenExpiration", "Role", "Username" },
                values: new object[] { 1L, new DateTime(2023, 10, 12, 15, 1, 57, 309, DateTimeKind.Utc), "kismarczirobi@gmail.com", new DateTime(2023, 10, 12, 15, 1, 57, 309, DateTimeKind.Utc), "$2a$11$RWyTguQbVXULKL/cNTEPXuzWEHiyt/37iti6hPOUFbgUQ/qir7YHS", null, null, "Admin", "kismarczi-admin" });

            migrationBuilder.CreateIndex(
                name: "IX_LearnStatistics_UserId",
                table: "LearnStatistics",
                column: "UserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Word_UserId",
                table: "Word",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LearnStatistics");

            migrationBuilder.DropTable(
                name: "Word");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
