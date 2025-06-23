using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddSubjectsAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5eac82ae-0b4b-47a8-9871-ba6ab1c99df7"), "Gjeografi" },
                    { new Guid("616273bd-2a2a-4894-b689-57fe86702ae0"), "Matematik" },
                    { new Guid("a072e5ed-714d-40d3-9af8-3b5b940acd2f"), "Anglisht" },
                    { new Guid("a5cf5e27-ef08-4fef-b907-109496b284eb"), "Histori" },
                    { new Guid("dbe6757d-2138-463a-bee7-5d07a6d7b320"), "Letërsi" },
                    { new Guid("faf6b93a-91d1-4ead-85f5-0120ac85f7d2"), "Shkenca" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 21, 18, 28, 57, 427, DateTimeKind.Utc).AddTicks(1880), "$2a$12$I13PGFsUfWd6FC9fsANR8eBrBNwjT/hwCpRdcj5lqmzvZyo4dc6za" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 16, 9, 56, 15, 112, DateTimeKind.Utc).AddTicks(982), "$2a$12$ZSBbhUkRbzrvuwWJO2zGmO/KQB0ayTaenA06nh8NinbicAiEEBQdC" });
        }
    }
}
