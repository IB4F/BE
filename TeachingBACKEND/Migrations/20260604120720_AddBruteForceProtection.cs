using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddBruteForceProtection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FailedLoginAttempts",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LockoutUntil",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3700), new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3700) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3710), new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3710) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3710), new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3710) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3720), new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3730) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3740), new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3740) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3750), new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3750) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3770), new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3770) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3780), new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3780) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3790), new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3790) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3800), new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3800) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3810), new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3810) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3820), new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3820) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3820), new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3820) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3830), new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3830) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3830), new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3830) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3840), new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3840) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3850), new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3850) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3850), new DateTime(2026, 6, 4, 12, 7, 19, 354, DateTimeKind.Utc).AddTicks(3850) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "FailedLoginAttempts", "LockoutUntil", "PasswordHash" },
                values: new object[] { new DateTime(2026, 6, 4, 12, 7, 19, 725, DateTimeKind.Utc).AddTicks(5900), 0, null, "$2a$12$KwacF/3k6uwzEMxXwHul1eFDMy0CGRqYmGjui1MEUnwuLcG4PpTh." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailedLoginAttempts",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LockoutUntil",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7100), new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7100) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7110), new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7110) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7110), new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7110) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7120), new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7120) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7130), new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7130) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7130), new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7130) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7160), new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7160) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7160), new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7160) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7170), new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7170) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7180), new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7180) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7190), new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7190) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7190), new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7190) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7200), new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7200) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7200), new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7200) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7220), new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7220) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7220), new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7220) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7230), new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7230) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7230), new DateTime(2026, 6, 3, 20, 14, 47, 281, DateTimeKind.Utc).AddTicks(7230) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 6, 3, 20, 14, 47, 629, DateTimeKind.Utc).AddTicks(8570), "$2a$12$QEForlnCrReF9FwMJay0NepP/UmmIsLz0o1hUOhsb/CT4OgX11p1C" });
        }
    }
}
