using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEstimatedStudentCountFromSupervisorApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedStudentCount",
                table: "SupervisorApplications");

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2100), new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2100) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2100), new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2100) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2110), new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2110) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2110), new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2110) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2130), new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2130) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2130), new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2130) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2170), new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2170) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2180), new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2180) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2180), new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2180) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2190), new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2190) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2200), new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2200) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2200), new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2200) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2210), new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2210) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2210), new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2210) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2210), new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2210) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2220), new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2220) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2220), new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2220) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2230), new DateTime(2025, 9, 16, 13, 5, 3, 571, DateTimeKind.Utc).AddTicks(2230) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 16, 13, 5, 3, 935, DateTimeKind.Utc).AddTicks(5900), "$2a$12$kNZY8YAUI7xU3aR8a5P01uPBDr489Ae.99lx6N4hCGvhF3ZimHk1W" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstimatedStudentCount",
                table: "SupervisorApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 15, 19, 20, 59, 502, DateTimeKind.Utc).AddTicks(9700), "$2a$12$W7lkwspiW2QxVqZgIPpkkubu.SlmqN6yE3FmYfuJSW/G5bCrNIY1y" });
        }
    }
}
