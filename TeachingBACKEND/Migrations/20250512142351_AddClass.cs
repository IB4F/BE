using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("013d0df5-50ef-4269-8a12-9b4f91ef07e1"), "Klasa 12" },
                    { new Guid("10840373-f0f4-4b10-9ee0-c5a831b6cf6a"), "Klasa 5" },
                    { new Guid("1c0b8bb7-9eb9-4a4d-8c4d-67de8057ae49"), "Klasa 10" },
                    { new Guid("3402fc90-d7be-420e-a980-2ff430d84838"), "Klasa 11" },
                    { new Guid("43e7b804-0e1a-4c82-9e44-6a194ee1ff63"), "Klasa 8" },
                    { new Guid("7fc8018d-6310-4c1f-b878-4b3a5b0b265c"), "Klasa 6" },
                    { new Guid("81bce1db-4f7c-4f6f-9e59-cde56a8200b6"), "Klasa 3" },
                    { new Guid("8e77a87f-e42b-487a-9cde-54f648c8c457"), "Klasa 7" },
                    { new Guid("a61d58b7-23f8-48f7-9778-3e048c5808a0"), "Klasa 2" },
                    { new Guid("e3f9a8f1-9c4e-4a91-8bcb-0b6b1583d3a1"), "Klasa 1" },
                    { new Guid("f82a2ea4-c4c9-4895-9484-0197a299c02f"), "Klasa 4" },
                    { new Guid("fd4b14ea-1b79-4e4c-83e0-0196c55b4bc1"), "Klasa 9" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "$2a$12$XpZJm5zckY2vGEaZf4JuQOyl9ORvUYvTH1CQGV6gUweyXDYpFcMiG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "$2a$12$6SPhwV64Lpb1/OdZIkXAH.r5/fXJJUgQsVBCCycmRW/.F5N6wA7M2");
        }
    }
}
