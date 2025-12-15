using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSubjectNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("616273bd-2a2a-4894-b689-57fe86702ae0"),
                column: "Name",
                value: "Matematikë");

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("faf6b93a-91d1-4ead-85f5-0120ac85f7d2"),
                column: "Name",
                value: "Shkencë");

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(8930), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(8930) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(8950), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(8950) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(8960), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(8960) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9030) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9060), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9060) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9070), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9070) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9140), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9140) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9140), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9140) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9150), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9150) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9160), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9160) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9170), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9170) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9180), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9180) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9200), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9200) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9200), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9200) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9210), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9210) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9220), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9220) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9220), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9220) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9230), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9230) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 10, 181, DateTimeKind.Utc).AddTicks(6080), "$2a$12$qwph3VXS/FWgLaZxgNAvSuUk4033e5b/gWlnkMElqTeuD28w1rG3y" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("616273bd-2a2a-4894-b689-57fe86702ae0"),
                column: "Name",
                value: "Matematik");

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("faf6b93a-91d1-4ead-85f5-0120ac85f7d2"),
                column: "Name",
                value: "Shkenca");

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
    }
}
