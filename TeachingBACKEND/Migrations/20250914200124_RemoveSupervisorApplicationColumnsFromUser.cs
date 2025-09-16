using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSupervisorApplicationColumnsFromUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSupervisorApplicationSubmitted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsSupervisorApproved",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SupervisorApplicationDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SupervisorApprovalDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TemporaryPassword",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSupervisorApplicationSubmitted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSupervisorApproved",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SupervisorApplicationDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SupervisorApprovalDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

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
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1730), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1730) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1740), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1740) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1750), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1750) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1750), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1750) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1770), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1770) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1770), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1770) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1800), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1800) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1810), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1810) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1810), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1810) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1820), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1820) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1830), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1830) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1830), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1830) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1850), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1850) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1850), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1850) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1860), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1860) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "IsSupervisorApplicationSubmitted", "IsSupervisorApproved", "PasswordHash", "SupervisorApplicationDate", "SupervisorApprovalDate", "TemporaryPassword" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 3, 1, DateTimeKind.Utc).AddTicks(4810), false, false, "$2a$12$AmTRzxRSAASBswRuWvcvwurPs6y5w7Jb2MRIxzQDgsNixDLuxgyFW", null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role_IsSupervisorApproved",
                table: "Users",
                columns: new[] { "Role", "IsSupervisorApproved" });
        }
    }
}
