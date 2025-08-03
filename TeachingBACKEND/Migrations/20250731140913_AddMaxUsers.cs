using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddMaxUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxUsers",
                table: "RegistrationPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-1111-1111-111111111111"),
                column: "MaxUsers",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-2222-2222-222222222222"),
                column: "MaxUsers",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-3333-3333-333333333333"),
                column: "MaxUsers",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-4444-4444-444444444444"),
                column: "MaxUsers",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-5555-5555-555555555555"),
                column: "MaxUsers",
                value: 5);

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-6666-6666-666666666666"),
                column: "MaxUsers",
                value: 10);

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-7777-7777-777777777777"),
                column: "MaxUsers",
                value: 50);

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-8888-8888-888888888888"),
                column: "MaxUsers",
                value: 500);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 31, 14, 9, 13, 636, DateTimeKind.Utc).AddTicks(9533), "$2a$12$icvPf0P9t8v597BB0NIzFexb6ExfBsFIi.y0FPgEtOvS.O/tnH6x." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxUsers",
                table: "RegistrationPlans");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 31, 11, 29, 12, 552, DateTimeKind.Utc).AddTicks(1808), "$2a$12$OKnhPXCaJuOhKXh40QApk.yE.ok8BRTvJiTRsIzdabBYa/ULd0mRW" });
        }
    }
}
