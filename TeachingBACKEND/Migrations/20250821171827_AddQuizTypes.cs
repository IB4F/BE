using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddQuizTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuizTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "QuizTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("b1b1b1b1-b1b1-1111-1111-111111111111"), "text" },
                    { new Guid("b2b2b2b2-b2b2-2222-2222-222222222222"), "imazhe" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 21, 17, 18, 27, 9, DateTimeKind.Utc).AddTicks(9850), "$2a$12$Iisuhi873n9k09G6TGeHx.yuj8acLNdp7dudoDV4bT5Ag0cmmeBbW" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizTypes");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 11, 7, 55, 58, 489, DateTimeKind.Utc).AddTicks(6801), "$2a$12$OHYokB/NX5sIt7qrQAQle.r3Nfr2Q1mRr.2c2Zf8L7gIedHbcRpQG" });
        }
    }
}
