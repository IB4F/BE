using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddTemporaryPasswordToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TemporaryPassword",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1670) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1680) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1680) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1690) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1710) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1710) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1730), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1740) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1740), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1740) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1750), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1750) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1760), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1760) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1760), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1760) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1770), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1770) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1780), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1780) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1780), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1780) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1800), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1800) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1800), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1800) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1810), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1810) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1810), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1810) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash", "TemporaryPassword" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 782, DateTimeKind.Utc).AddTicks(8680), "$2a$12$X/Mc0Ym2X0LuJ5WgReGGs.2Ig4aiAcnALODjdmb62Dq4.xAqVqEii", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TemporaryPassword",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3460), new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3560), new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3560) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3570), new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3570) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3580) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3590), new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3590) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3600), new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3600) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3600), new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3600) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3610), new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3610) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3620), new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3620) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3620), new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3620) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3630), new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3630) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3630), new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3630) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3640), new DateTime(2025, 9, 14, 18, 56, 1, 994, DateTimeKind.Utc).AddTicks(3640) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 56, 2, 374, DateTimeKind.Utc).AddTicks(6970), "$2a$12$D1stUr96/9eep4hQYlIgd.x91at5ffg9hg10Om1A7avZu9LwmShpO" });
        }
    }
}
