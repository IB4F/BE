using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddMultiProviderPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalCustomerId",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalSubscriptionId",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Provider",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "StripeYearlyPriceId",
                table: "SubscriptionPackages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "StripeMonthlyPriceId",
                table: "SubscriptionPackages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PaddleMonthlyPriceId",
                table: "SubscriptionPackages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaddleYearlyPriceId",
                table: "SubscriptionPackages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StripeSessionId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ExternalSessionId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Provider",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PaddleMonthlyPriceId", "PaddleYearlyPriceId", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(410), null, null, new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(410) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PaddleMonthlyPriceId", "PaddleYearlyPriceId", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(410), null, null, new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(420) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PaddleMonthlyPriceId", "PaddleYearlyPriceId", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(420), null, null, new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(420) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PaddleMonthlyPriceId", "PaddleYearlyPriceId", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(420), null, null, new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(420) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PaddleMonthlyPriceId", "PaddleYearlyPriceId", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(440), null, null, new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(440) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PaddleMonthlyPriceId", "PaddleYearlyPriceId", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(440), null, null, new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(440) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PaddleMonthlyPriceId", "PaddleYearlyPriceId", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(480), null, null, new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(490) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PaddleMonthlyPriceId", "PaddleYearlyPriceId", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(490), null, null, new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(490) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PaddleMonthlyPriceId", "PaddleYearlyPriceId", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(500), null, null, new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(500) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PaddleMonthlyPriceId", "PaddleYearlyPriceId", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(500), null, null, new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(510) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PaddleMonthlyPriceId", "PaddleYearlyPriceId", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(510), null, null, new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(510) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PaddleMonthlyPriceId", "PaddleYearlyPriceId", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(520), null, null, new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(520) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PaddleMonthlyPriceId", "PaddleYearlyPriceId", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(520), null, null, new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(520) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PaddleMonthlyPriceId", "PaddleYearlyPriceId", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(550), null, null, new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(550) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PaddleMonthlyPriceId", "PaddleYearlyPriceId", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(560), null, null, new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(560) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PaddleMonthlyPriceId", "PaddleYearlyPriceId", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(560), null, null, new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(560) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "PaddleMonthlyPriceId", "PaddleYearlyPriceId", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(570), null, null, new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(570) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "PaddleMonthlyPriceId", "PaddleYearlyPriceId", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(570), null, null, new DateTime(2026, 4, 17, 21, 41, 50, 55, DateTimeKind.Utc).AddTicks(570) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 4, 17, 21, 41, 50, 426, DateTimeKind.Utc).AddTicks(8890), "$2a$12$dPttm3MwYk2RFGwMo3gJn.b.VBsjakVtICGwx2q97MsWZ65VtihPq" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalCustomerId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "ExternalSubscriptionId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Provider",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PaddleMonthlyPriceId",
                table: "SubscriptionPackages");

            migrationBuilder.DropColumn(
                name: "PaddleYearlyPriceId",
                table: "SubscriptionPackages");

            migrationBuilder.DropColumn(
                name: "ExternalSessionId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Provider",
                table: "Payments");

            migrationBuilder.AlterColumn<string>(
                name: "StripeYearlyPriceId",
                table: "SubscriptionPackages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StripeMonthlyPriceId",
                table: "SubscriptionPackages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StripeSessionId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7640), new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7640) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7650), new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7650) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7650), new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7650) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7650), new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7660) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7660), new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7660) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7690), new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7690) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7710), new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7710) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7720), new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7720), new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7730), new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7730) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7740), new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7740) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7750), new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7750) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7750), new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7750) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7760), new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7760) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7760), new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7760) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7760), new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7770) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7770), new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7770) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7770), new DateTime(2026, 4, 3, 14, 55, 57, 131, DateTimeKind.Utc).AddTicks(7770) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 4, 3, 14, 55, 57, 498, DateTimeKind.Utc).AddTicks(8580), "$2a$12$5ps9dXHku.N88Hc3qXX1G.goNHOo/bU7LPxTY5SXyYnvtytVck9pO" });
        }
    }
}
