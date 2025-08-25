using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddParentChildQuizRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentQuizId",
                table: "Quizzes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 24, 19, 20, 12, 165, DateTimeKind.Utc).AddTicks(7410), "$2a$12$M.L/kQ8OyuqP3PPpsFFSz.b/cpdUax9byi63zcmFFZEBDHdjTlbR2" });

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_ParentQuizId",
                table: "Quizzes",
                column: "ParentQuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Quizzes_ParentQuizId",
                table: "Quizzes",
                column: "ParentQuizId",
                principalTable: "Quizzes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Quizzes_ParentQuizId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_ParentQuizId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "ParentQuizId",
                table: "Quizzes");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 21, 20, 15, 20, 122, DateTimeKind.Utc).AddTicks(3090), "$2a$12$lvdxp7dbPKD1Y0s4n59Sp.O9u8NvHYfdYMM6I7M50PPRCAmaydR5y" });
        }
    }
}
