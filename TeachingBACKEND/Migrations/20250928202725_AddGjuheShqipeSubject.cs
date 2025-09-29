using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddGjuheShqipeSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("b7a00956-0bfb-4013-af86-fc5848115def"), "Gjuhë Shqipe" });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9470), new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9470) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9470), new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9480) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9480), new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9480) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9480), new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9480) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9490), new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9490) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9500), new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9500) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9520), new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9520) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9530), new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9530) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9540), new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9540) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9540), new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9540) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9570) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9580) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9580) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9590), new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9590) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9590), new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9590) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9600) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9600) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 9, 28, 20, 27, 23, 273, DateTimeKind.Utc).AddTicks(9610) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 28, 20, 27, 23, 666, DateTimeKind.Utc).AddTicks(6530), "$2a$12$O/GNqymu1bujNamQNPe0POyEAR8PuUGK6TwmhHqM6DPEdZiOO7fgK" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("b7a00956-0bfb-4013-af86-fc5848115def"));

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6280), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6280) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6290), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6290) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6290), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6290) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6300), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6300) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6310), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6310) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6320), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6320) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6350), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6350) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6350), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6350) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6360), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6360) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6370), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6370) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6370), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6370) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6380), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6380) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6400), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6400), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6410) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6410), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6410) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6410), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6410) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 36, 79, DateTimeKind.Utc).AddTicks(4670), "$2a$12$H5Fhr2HdxFuC/3aIH0t91ePJntHKz/KFrPhMYrzA/hfLbXMRkZDJK" });
        }
    }
}
