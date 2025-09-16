using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddOriginalGeneratedPasswordField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OriginalGeneratedPassword",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(6960), new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(6960) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(6970), new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(6970) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(6970), new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(6970) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(6980), new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(6980) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7010), new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7010) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7010), new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7010) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7060), new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7060) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7070), new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7070) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7070), new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7070) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7080), new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7090) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7100), new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7100) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7100), new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7100) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7110), new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7110) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7110), new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7110) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7120), new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7120) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7120), new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7120) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7130), new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7130) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7140), new DateTime(2025, 9, 15, 19, 20, 59, 68, DateTimeKind.Utc).AddTicks(7140) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "OriginalGeneratedPassword", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 15, 19, 20, 59, 502, DateTimeKind.Utc).AddTicks(9700), null, "$2a$12$W7lkwspiW2QxVqZgIPpkkubu.SlmqN6yE3FmYfuJSW/G5bCrNIY1y" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalGeneratedPassword",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5800), new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5800) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5810), new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5810) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5810), new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5810) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5820), new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5820) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5830), new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5830) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5830), new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5860), new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5860) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5870), new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5870) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5880), new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5880) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5880), new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5880) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 9, 14, 20, 1, 23, 338, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 14, 20, 1, 23, 706, DateTimeKind.Utc).AddTicks(9240), "$2a$12$UtSd6jvTTVyHCSskH0JexOqr8vRlWangJl/7KQjteWHb7WvfAYDxe" });
        }
    }
}
