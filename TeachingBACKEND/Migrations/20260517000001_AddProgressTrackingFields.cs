using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using TeachingBACKEND.Data;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20260517000001_AddProgressTrackingFields")]
    public partial class AddProgressTrackingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PenaltyPoints",
                table: "StudentPerformanceSummaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswerQuiz",
                table: "StudentPerformanceSummaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "LastCompletedQuizId",
                table: "StudentPerformanceSummaries",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PenaltyPoints",
                table: "StudentPerformanceSummaries");

            migrationBuilder.DropColumn(
                name: "CorrectAnswerQuiz",
                table: "StudentPerformanceSummaries");

            migrationBuilder.DropColumn(
                name: "LastCompletedQuizId",
                table: "StudentPerformanceSummaries");
        }
    }
}
