using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTypePlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "RegistrationPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-1111-1111-111111111111"),
                column: "UserType",
                value: "student");

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-2222-2222-222222222222"),
                column: "UserType",
                value: "student");

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-3333-3333-333333333333"),
                column: "UserType",
                value: "student");

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-4444-4444-444444444444"),
                column: "UserType",
                value: "student");

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-5555-5555-555555555555"),
                column: "UserType",
                value: "family");

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-6666-6666-666666666666"),
                column: "UserType",
                value: "family");

            migrationBuilder.InsertData(
                table: "RegistrationPlans",
                columns: new[] { "Id", "IsFamilyPlan", "Price", "RegistrationPlanName", "StripeProductName", "Type", "UserType" },
                values: new object[,]
                {
                    { new Guid("a1a1a1a1-a1a1-7777-7777-777777777777"), false, 10000L, "Supervisor - Monthly", "Supervisor - Monthly Plan", "monthly", "supervisor" },
                    { new Guid("a1a1a1a1-a1a1-8888-8888-888888888888"), false, 100000L, "Supervisor - Yearly", "Supervisor - Yearly Plan", "yearly", "supervisor" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 31, 11, 29, 12, 552, DateTimeKind.Utc).AddTicks(1808), "$2a$12$OKnhPXCaJuOhKXh40QApk.yE.ok8BRTvJiTRsIzdabBYa/ULd0mRW" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-8888-8888-888888888888"));

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "RegistrationPlans");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 29, 12, 24, 33, 66, DateTimeKind.Utc).AddTicks(3200), "$2a$12$j2jHGyxGmnbYBSCt6dQlTuYSkzw9oAOco3ReAP/vxonw/nRYl36XG" });
        }
    }
}
