using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class MakeHashNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 9, 14, 41, 40, 715, DateTimeKind.Utc).AddTicks(6430), "$2a$12$lzEft92rDHsGKFBi7ugJ6u.A7bjdOK5xOej02LwSuuwUH8O09ev8u" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 9, 9, 53, 57, 43, DateTimeKind.Utc).AddTicks(6754), "$2a$12$CfZ13W1sSPOoQulEW5u8QORpbB/W50QYDaLwibvqVlN6Sz/y/8wIq" });
        }
    }
}
