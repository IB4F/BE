using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeTrackingToStudentQuizAttempts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StartedAt",
                table: "StudentQuizAttempts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TimeSpentSeconds",
                table: "StudentQuizAttempts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 1, 19, 12, 56, 611, DateTimeKind.Utc).AddTicks(4490), "$2a$12$2vtEwPpF4oak0WJtHF7y3.6.//.hr6d027bZZIPYYOrz.OSuAXpQ." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartedAt",
                table: "StudentQuizAttempts");

            migrationBuilder.DropColumn(
                name: "TimeSpentSeconds",
                table: "StudentQuizAttempts");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 1, 18, 57, 13, 106, DateTimeKind.Utc).AddTicks(6640), "$2a$12$I3NXUZGihWksfJdxRIn34eZtcg1esPeX/GA1GizSPHzAb44cFeyW2" });
        }
    }
}
