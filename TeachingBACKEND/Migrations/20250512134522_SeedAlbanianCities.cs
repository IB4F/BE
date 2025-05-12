using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class SeedAlbanianCities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("12f4c32b-4a42-4b1f-9247-9b40efb21363"), "Fier" },
                    { new Guid("2dff0b99-3d86-47a5-b7ad-4e6d3c0ec748"), "Elbasan" },
                    { new Guid("69e02c84-4a61-406e-844d-24df2e25a983"), "Gjirokastër" },
                    { new Guid("8e065f0d-71c1-4a63-804b-49b1d08c1407"), "Berat" },
                    { new Guid("a2d4a4ee-5fa2-4a33-bd3c-2bbf98e9310b"), "Tirana" },
                    { new Guid("b8e13e5a-bba4-48b6-99d6-c4f123ab2cb3"), "Durrës" },
                    { new Guid("bc4e14b5-7d7e-4b6c-8b33-eaa2c91f9015"), "Vlorë" },
                    { new Guid("d05f0e99-20f0-4c9a-b03f-3ea92ec02b41"), "Shkodër" },
                    { new Guid("d28f0be5-5f17-4ec8-b365-7387c22234e9"), "Lezhë" },
                    { new Guid("e5f957d6-7a31-45b2-bded-44e4992d4b83"), "Kukës" },
                    { new Guid("f8d8e184-74c0-4551-b66b-7ae3f05e7ff5"), "Korçë" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "$2a$12$6SPhwV64Lpb1/OdZIkXAH.r5/fXJJUgQsVBCCycmRW/.F5N6wA7M2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("12f4c32b-4a42-4b1f-9247-9b40efb21363"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("2dff0b99-3d86-47a5-b7ad-4e6d3c0ec748"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("69e02c84-4a61-406e-844d-24df2e25a983"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("8e065f0d-71c1-4a63-804b-49b1d08c1407"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("a2d4a4ee-5fa2-4a33-bd3c-2bbf98e9310b"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("b8e13e5a-bba4-48b6-99d6-c4f123ab2cb3"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("bc4e14b5-7d7e-4b6c-8b33-eaa2c91f9015"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d05f0e99-20f0-4c9a-b03f-3ea92ec02b41"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d28f0be5-5f17-4ec8-b365-7387c22234e9"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("e5f957d6-7a31-45b2-bded-44e4992d4b83"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f8d8e184-74c0-4551-b66b-7ae3f05e7ff5"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "$2a$12$opbqW6ooNhpkAWw6CfEX9OlvklHubsF19LAtPwbwyo1XNrvhH6ote");
        }
    }
}
