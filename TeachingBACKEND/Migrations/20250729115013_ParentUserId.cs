using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class ParentUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentUserId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFamilyPlan",
                table: "RegistrationPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-1111-1111-111111111111"),
                column: "IsFamilyPlan",
                value: false);

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-2222-2222-222222222222"),
                column: "IsFamilyPlan",
                value: false);

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-3333-3333-333333333333"),
                column: "IsFamilyPlan",
                value: false);

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-4444-4444-444444444444"),
                column: "IsFamilyPlan",
                value: false);

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-5555-5555-555555555555"),
                column: "IsFamilyPlan",
                value: false);

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-6666-6666-666666666666"),
                column: "IsFamilyPlan",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "ParentUserId", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 29, 11, 50, 13, 513, DateTimeKind.Utc).AddTicks(1343), null, "$2a$12$pwY29ac1JpMoV.J9jf8V4e3vWTWjhG/15m9sN9zKZzqcLd9bR6fGq" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsFamilyPlan",
                table: "RegistrationPlans");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 16, 12, 47, 4, 529, DateTimeKind.Utc).AddTicks(8720), "$2a$12$mhRUo9401kZa8NCSQ.3qT.2fMbfJL/zqJqf24r1tX9OeJ42vTJ6pO" });
        }
    }
}
