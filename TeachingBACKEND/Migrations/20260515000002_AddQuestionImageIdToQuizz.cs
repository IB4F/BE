using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using TeachingBACKEND.Data;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20260515000002_AddQuestionImageIdToQuizz")]
    public partial class AddQuestionImageIdToQuizz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "QuestionImageId",
                table: "Quizzes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_QuestionImageId",
                table: "Quizzes",
                column: "QuestionImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Files_QuestionImageId",
                table: "Quizzes",
                column: "QuestionImageId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Files_QuestionImageId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_QuestionImageId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "QuestionImageId",
                table: "Quizzes");
        }
    }
}
