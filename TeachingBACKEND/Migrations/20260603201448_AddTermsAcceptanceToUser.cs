using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddTermsAcceptanceToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TermsAcceptedAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TermsVersion",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

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
                columns: new[] { "CreateAt", "PasswordHash", "TermsAcceptedAt", "TermsVersion" },
                values: new object[] { new DateTime(2026, 6, 3, 20, 14, 47, 629, DateTimeKind.Utc).AddTicks(8570), "$2a$12$QEForlnCrReF9FwMJay0NepP/UmmIsLz0o1hUOhsb/CT4OgX11p1C", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TermsAcceptedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TermsVersion",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(340), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(340) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(340), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(340) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(350), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(350) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(350), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(360) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(360), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(360) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(370), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(370) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(390), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(390) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(400), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(400) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(400), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(400) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(410), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(410) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(420), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(420) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(420), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(420) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(430), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(430) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(430), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(430) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(440), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(440) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(460), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(460) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(460), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(460) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(470), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(470) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 999, DateTimeKind.Utc).AddTicks(5700), "$2a$12$cx0EFaWd1br6NciVXt7AHOsHKw5jolpRB8marijGPv/A6T.67tvS." });
        }
    }
}
