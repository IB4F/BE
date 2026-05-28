using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddOrphanedFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrphanedFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrphanedFiles", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1780), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1780) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1790), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1790) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1790), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1790) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1800), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1800) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1810), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1810) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1810), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1810) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1850), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1850) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1860), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1860) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1870), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1870) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1870), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1870) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1880), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1880) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1880), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1880) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1890), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1890) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1890), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1890) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1900), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1900) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1910), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1910) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1910), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1910) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1920), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1920) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 364, DateTimeKind.Utc).AddTicks(6580), "$2a$12$TGmbCNTKIqLamqSfy.LYLuhadkLUhUGqP9jDRlh6hvhtB2csVfJ/2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrphanedFiles");

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2630), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2630) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2640), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2640) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 497, DateTimeKind.Utc).AddTicks(4490), "$2a$12$OxRL9/6NcNGzk.UIMVct1.468ypYe6Hepuu0c9HShvwtGSEbWUajC" });
        }
    }
}
