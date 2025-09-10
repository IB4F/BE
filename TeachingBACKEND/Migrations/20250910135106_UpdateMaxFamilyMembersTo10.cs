using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMaxFamilyMembersTo10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(270), new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(270) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(270), new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(270) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(280), new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(280) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(280), new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(280) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(300), new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(300) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(300), new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(300) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "MaxFamilyMembers", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(330), 10, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(330) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "MaxFamilyMembers", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(340), 10, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(340) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(340), new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(340) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(350), new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(350) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "MaxFamilyMembers", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(350), 10, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(350) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "MaxFamilyMembers", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(360), 10, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(360) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(360), new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(370) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(370), new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(370) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(370), new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(370) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(380), new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(380) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(380), new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(380) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(390), new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(390) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 5, 66, DateTimeKind.Utc).AddTicks(4020), "$2a$12$71TElqWj7KZzwGW24fl/weyUeCAUe.r/ytbMsSAma1aZ6GWJkmZYW" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2370), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2370) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2370), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2370) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2380), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2380) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2380), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2380) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2400) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2400) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "MaxFamilyMembers", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2420), 5, new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2420) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "MaxFamilyMembers", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2530), 5, new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2530) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2540), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2540) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2540), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2540) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "MaxFamilyMembers", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2550), 15, new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2550) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "MaxFamilyMembers", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2560), 15, new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2560) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2560), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2560) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2560), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2560) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2570), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2570) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2570), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2570) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2580), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2580) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2580), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2580) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 9, 204, DateTimeKind.Utc).AddTicks(6520), "$2a$12$SHHSi0YUlTNR9gblE0EdiuzZnp8fQyhgZ891.BknEZKWXUcefbg/G" });
        }
    }
}
