using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class MakeStripeInvoiceIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StripeInvoiceId",
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
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2630), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2630) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2640), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2640) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2640), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2640) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2650), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2650) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2660), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2660) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2670), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2700), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2710), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2710), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2720), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2720), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2730), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2730), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2740) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2740), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2740) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2740), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2740) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2750), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2750), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2760), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 497, DateTimeKind.Utc).AddTicks(4490), "$2a$12$OxRL9/6NcNGzk.UIMVct1.468ypYe6Hepuu0c9HShvwtGSEbWUajC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StripeInvoiceId",
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
    }
}
