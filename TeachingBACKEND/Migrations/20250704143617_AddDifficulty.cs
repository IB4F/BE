using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddDifficulty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "LearnHubs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 4, 14, 36, 16, 699, DateTimeKind.Utc).AddTicks(4083), "$2a$12$.DlYhLJPhLR4586C55AFC.Gw/ON3upLv3KIm8OgwOHMVRZ0WMw2oW" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "LearnHubs");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 21, 18, 28, 57, 427, DateTimeKind.Utc).AddTicks(1880), "$2a$12$I13PGFsUfWd6FC9fsANR8eBrBNwjT/hwCpRdcj5lqmzvZyo4dc6za" });
        }
    }
}
