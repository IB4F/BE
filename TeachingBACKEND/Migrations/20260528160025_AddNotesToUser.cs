using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddNotesToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

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
                columns: new[] { "CreateAt", "Notes", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 28, 16, 0, 23, 951, DateTimeKind.Utc).AddTicks(2400), null, "$2a$12$8PJdYU58VsXpYELylTEpcegpJmLyDJt9Vz4EqGYaHYljpqk1g9toO" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1780), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1780) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1790), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1790) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1790), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1790) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1800), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1800) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1810), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1810) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1810), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1810) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1850), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1850) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1860), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1860) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1870), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1870) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1870), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1870) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1880), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1880) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1880), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1880) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1890), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1890) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1890), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1890) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1900), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1900) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1910), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1910) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1910), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1910) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1920), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1920) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 364, DateTimeKind.Utc).AddTicks(6580), "$2a$12$TGmbCNTKIqLamqSfy.LYLuhadkLUhUGqP9jDRlh6hvhtB2csVfJ/2" });
        }
    }
}
