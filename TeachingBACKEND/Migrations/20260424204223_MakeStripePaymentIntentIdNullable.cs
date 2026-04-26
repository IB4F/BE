using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class MakeStripePaymentIntentIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StripePaymentIntentId",
                table: "SubscriptionPayments",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6110), new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6110) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6110), new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6110) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6120), new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6120) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6120), new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6120) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6130), new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6130) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6140), new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6140) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6180), new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6180) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6190), new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6190) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6190), new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6200) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6200), new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6200) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6210), new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6210) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6210), new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6210) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6220), new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6220) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6220), new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6220) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6230), new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6230) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6230), new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6240) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6240), new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6240) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6250), new DateTime(2026, 4, 24, 20, 42, 21, 859, DateTimeKind.Utc).AddTicks(6260) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 42, 22, 221, DateTimeKind.Utc).AddTicks(3910), "$2a$12$vlAVS8EcxHJISFBc0U7NaO3WjGfInj0.oIzebTh7o66h21Its4C/K" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StripePaymentIntentId",
                table: "SubscriptionPayments",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5280), new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5300), new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5300), new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5310), new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5310) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5320), new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5320) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5330), new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5330) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5360), new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5370) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5370), new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5370) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5390), new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5390) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5390), new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5400) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5400), new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5400) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5410), new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5410) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5420), new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5420) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5430), new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5430), new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5440), new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5450), new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5450), new DateTime(2026, 4, 24, 19, 28, 40, 223, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 4, 24, 19, 28, 40, 593, DateTimeKind.Utc).AddTicks(1690), "$2a$12$lxAomPGPo7Xwuzhoh0CFxubRZ5XwT28wYB32vYLJS2Uv1DZMW2nzy" });
        }
    }
}
