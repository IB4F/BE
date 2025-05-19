using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "$2a$12$XVlfnhqvTQVTeEebDDMqgeXKCb7uFPI6jK8JoeMilBLqPDti7N6Bq");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "$2a$12$NGdgHR9QTdScxYW/BWASH.VG3wNEu9.c0U3KWSWSHIx5IUBFNbrYG");
        }
    }
}
