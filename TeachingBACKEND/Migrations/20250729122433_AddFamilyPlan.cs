using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddFamilyPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-5555-5555-555555555555"),
                column: "IsFamilyPlan",
                value: true);

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-6666-6666-666666666666"),
                column: "IsFamilyPlan",
                value: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 24, 33, 66, DateTimeKind.Utc).AddTicks(3200), "$2a$12$j2jHGyxGmnbYBSCt6dQlTuYSkzw9oAOco3ReAP/vxonw/nRYl36XG" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 29, 11, 50, 13, 513, DateTimeKind.Utc).AddTicks(1343), "$2a$12$pwY29ac1JpMoV.J9jf8V4e3vWTWjhG/15m9sN9zKZzqcLd9bR6fGq" });
        }
    }
}
