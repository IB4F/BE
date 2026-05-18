using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using TeachingBACKEND.Data;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20260517000002_AddStudentQuizResults")]
    public partial class AddStudentQuizResults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentQuizResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    PointsDelta = table.Column<int>(type: "int", nullable: false),
                    HasChildQuiz = table.Column<bool>(type: "bit", nullable: false),
                    AttemptedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentQuizResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentQuizResults_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentQuizResults_Links_LinkId",
                        column: x => x.LinkId,
                        principalTable: "Links",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentQuizResults_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizResults_StudentId_QuizId",
                table: "StudentQuizResults",
                columns: new[] { "StudentId", "QuizId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizResults_LinkId",
                table: "StudentQuizResults",
                column: "LinkId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizResults_QuizId",
                table: "StudentQuizResults",
                column: "QuizId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "StudentQuizResults");
        }
    }
}
