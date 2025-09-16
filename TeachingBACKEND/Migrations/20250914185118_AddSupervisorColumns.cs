using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddSupervisorColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<bool>(
                name: "IsOneTimeLoginUsed",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.AddColumn<bool>(
                name: "MustChangePasswordOnNextLogin",
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

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5140), new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5140) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5150), new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5150) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5150), new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5150) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5160), new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5160) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5300), new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5300), new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5310), new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5310) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5310), new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5310) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5320), new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5320) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5320), new DateTime(2025, 9, 14, 18, 51, 16, 673, DateTimeKind.Utc).AddTicks(5320) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "IsOneTimeLoginUsed", "IsSupervisorApplicationSubmitted", "IsSupervisorApproved", "MustChangePasswordOnNextLogin", "PasswordHash", "SupervisorApplicationDate", "SupervisorApprovalDate" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 51, 17, 57, DateTimeKind.Utc).AddTicks(6780), false, false, false, false, "$2a$12$iA3boYMSHEyZLvJrw9zUzeD13rgDISAUPjeEu8iQnLct5rAQDjqUO", null, null });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOneTimeLoginUsed",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsSupervisorApplicationSubmitted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsSupervisorApproved",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MustChangePasswordOnNextLogin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SupervisorApplicationDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SupervisorApprovalDate",
                table: "Users");
        }
    }
}
