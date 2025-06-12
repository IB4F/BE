using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class MakeTokenNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 10, 9, 1, 36, 713, DateTimeKind.Utc).AddTicks(2484), "$2a$12$yZW2/y1FDMQAaPWHIcZaZuq42P6i3epKBnFJVPijXc2xSDSgHwuMq" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 10, 9, 1, 18, 653, DateTimeKind.Utc).AddTicks(5979), "$2a$12$LSkJ53Iq.gLB6k0jdly4wO7ufbpSxdkRVOauZeVJngYqgIfufy/Ya" });
        }
    }
}
