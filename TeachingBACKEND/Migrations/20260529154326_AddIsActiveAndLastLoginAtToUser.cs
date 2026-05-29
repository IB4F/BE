using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveAndLastLoginAtToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8410), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8440), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8440) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8440), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8440) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8450), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8460), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8460), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8500), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8500) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8510), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8510) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8510), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8510) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8520), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8520) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8520), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8530) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8530), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8530) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8530), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8540) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8540), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8540) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8540), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8540) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8550), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8550) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8560), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8560), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "IsActive", "LastLoginAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 912, DateTimeKind.Utc).AddTicks(8740), true, null, "$2a$12$xwwB/LaXoE0y38MkJrHMa..ErL8nl50dZAOld6unnoJuFbS0WFnPq" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastLoginAt",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7550), new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7550) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7560), new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7560) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7560), new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7560) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7570), new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7600), new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7610), new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7630), new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7640), new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7640) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7650), new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7650) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7650), new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7650) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7660), new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7660) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7670), new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7670) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7670), new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7670) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7680), new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7680), new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7690), new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7690) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7690), new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7690) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7700), new DateTime(2026, 5, 28, 16, 0, 23, 599, DateTimeKind.Utc).AddTicks(7700) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 28, 16, 0, 23, 951, DateTimeKind.Utc).AddTicks(2400), "$2a$12$8PJdYU58VsXpYELylTEpcegpJmLyDJt9Vz4EqGYaHYljpqk1g9toO" });
        }
    }
}
