using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOriginalGeneratedPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalGeneratedPassword",
                table: "Users");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4700), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4700), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4710), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4710), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4720), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4730), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4750), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4760), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4760) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4770), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4770) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4780), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4780) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4790), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4790) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4910), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4910), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4920), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4920) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4920), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4920) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4930), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4930) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4930), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4930) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4940), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4940) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "OriginalGeneratedPassword", "PasswordHash" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 56, 130, DateTimeKind.Utc).AddTicks(9840), null, "$2a$12$foSfyKVg8SzBlmkBqXvJ4eLPADpay8e66xfg2LV8SBLJwTxajrtxq" });
        }
    }
}
