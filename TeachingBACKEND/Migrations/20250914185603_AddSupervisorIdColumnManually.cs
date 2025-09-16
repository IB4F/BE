using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddSupervisorIdColumnManually : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SupervisorId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "Users");

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
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 14, 18, 51, 17, 57, DateTimeKind.Utc).AddTicks(6780), "$2a$12$iA3boYMSHEyZLvJrw9zUzeD13rgDISAUPjeEu8iQnLct5rAQDjqUO" });
        }
    }
}
