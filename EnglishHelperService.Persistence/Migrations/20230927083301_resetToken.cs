using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishHelperService.Persistence.Migrations
{
    public partial class resetToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActive",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 27, 10, 33, 0, 785, DateTimeKind.Local).AddTicks(1988),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 13, 22, 56, 44, 542, DateTimeKind.Local).AddTicks(8420));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 27, 10, 33, 0, 785, DateTimeKind.Local).AddTicks(1756),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 13, 22, 56, 44, 542, DateTimeKind.Local).AddTicks(8200));

            migrationBuilder.AddColumn<string>(
                name: "ResetToken",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpiration",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Email",
                value: "kismarczirobi@gmail.com");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetToken",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpiration",
                table: "User");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActive",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 13, 22, 56, 44, 542, DateTimeKind.Local).AddTicks(8420),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 27, 10, 33, 0, 785, DateTimeKind.Local).AddTicks(1988));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 13, 22, 56, 44, 542, DateTimeKind.Local).AddTicks(8200),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 27, 10, 33, 0, 785, DateTimeKind.Local).AddTicks(1756));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Email",
                value: "admin@example.com");
        }
    }
}
