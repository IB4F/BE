using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTrialDaysToZero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7420), 0, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7420) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7430), 0, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7430) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7440), 0, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7440) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7440), 0, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7440) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7450), 0, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7450) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7460), 0, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7460) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7490), 0, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7490) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7490), 0, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7490) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7500), 0, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7500) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7510), 0, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7510) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7510), 0, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7510) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7520), 0, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7520) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7520), 0, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7520) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7530), 0, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7530) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7530), 0, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7530) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7540), 0, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7540) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7540), 0, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7540) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7550), 0, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7550) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 9, 277, DateTimeKind.Utc).AddTicks(4380), "$2a$12$Tm7KTL5NIaj.jq0uo7JUFubvRGtnHsxTXr.KMvHMOE..m9VE3uBOm" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(270), 7, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(270) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(270), 7, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(270) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(280), 7, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(280) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(280), 7, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(280) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(300), 7, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(300) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(300), 7, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(300) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(330), 7, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(330) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(340), 7, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(340) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(340), 7, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(340) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(350), 7, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(350) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(350), 7, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(350) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(360), 7, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(360) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(360), 14, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(370) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(370), 14, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(370) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(370), 14, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(370) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(380), 14, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(380) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(380), 14, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(380) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "TrialDays", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(390), 14, new DateTime(2025, 9, 10, 13, 51, 4, 626, DateTimeKind.Utc).AddTicks(390) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 10, 13, 51, 5, 66, DateTimeKind.Utc).AddTicks(4020), "$2a$12$71TElqWj7KZzwGW24fl/weyUeCAUe.r/ytbMsSAma1aZ6GWJkmZYW" });
        }
    }
}
