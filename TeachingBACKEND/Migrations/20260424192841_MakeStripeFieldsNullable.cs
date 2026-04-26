using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class MakeStripeFieldsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StripeSubscriptionId",
                table: "Subscriptions",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "StripePriceId",
                table: "Subscriptions",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "StripeCustomerId",
                table: "Subscriptions",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StripeSubscriptionId",
                table: "Subscriptions",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StripePriceId",
                table: "Subscriptions",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StripeCustomerId",
                table: "Subscriptions",
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
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(410), new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(410) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(410), new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(420) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(420), new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(420) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(420), new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(420) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(440), new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(440) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(440), new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(440) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(480), new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(490) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(490), new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(490) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(500), new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(500) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(500), new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(510) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(510), new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(510) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(520), new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(520) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(520), new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(520) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(550), new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(550) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(560), new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(560) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(560), new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(560) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(570), new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(570) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(570), new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(570) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 426, DateTimeKind.Utc).AddTicks(8890), "$2a$12$dPttm3MwYk2RFGwMo3gJn.b.VBsjakVtICGwx2q97MsWZ65VtihPq" });
        }
    }
}
