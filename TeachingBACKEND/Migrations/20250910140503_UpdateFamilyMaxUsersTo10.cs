using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFamilyMaxUsersTo10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(400), new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(400) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(480), new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(480) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(480), new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(480) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(490), new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(490) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(500), new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(500) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(500), new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(500) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "MaxUsers", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(530), 10, new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(530) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "MaxUsers", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(540), 10, new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(540) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(550), new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(550) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(550), new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(550) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "MaxUsers", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(560), 10, new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(560) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "MaxUsers", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(570), 10, new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(570) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(570), new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(580) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(580), new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(580) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(580), new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(580) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(590), new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(590) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(590), new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(590) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(600), new DateTime(2025, 9, 10, 14, 5, 1, 807, DateTimeKind.Utc).AddTicks(600) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 5, 2, 230, DateTimeKind.Utc).AddTicks(2000), "$2a$12$Ieq8gQW8ohzu3BFHcSWdQOnrlbZMF.toHggK2mHYR2VOigI.p763y" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7420), new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7420) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7430), new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7430) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7440), new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7440) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7440), new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7440) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7450), new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7450) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7460), new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7460) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "MaxUsers", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7490), 5, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7490) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "MaxUsers", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7490), 5, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7490) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7500), new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7500) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7510), new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7510) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "MaxUsers", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7510), 15, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7510) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "MaxUsers", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7520), 15, new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7520) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7520), new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7520) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7530), new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7530) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7530), new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7530) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7540), new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7540) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7540), new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7540) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7550), new DateTime(2025, 9, 10, 14, 2, 8, 873, DateTimeKind.Utc).AddTicks(7550) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 10, 14, 2, 9, 277, DateTimeKind.Utc).AddTicks(4380), "$2a$12$Tm7KTL5NIaj.jq0uo7JUFubvRGtnHsxTXr.KMvHMOE..m9VE3uBOm" });
        }
    }
}
