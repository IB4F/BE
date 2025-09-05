using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class UpdateToScalableStudentPerformance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentQuizAttempts");

            migrationBuilder.CreateTable(
                name: "StudentPerformanceSummaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalQuizzes = table.Column<int>(type: "int", nullable: false),
                    CompletedQuizzes = table.Column<int>(type: "int", nullable: false),
                    TotalPointsEarned = table.Column<int>(type: "int", nullable: false),
                    TotalPossiblePoints = table.Column<int>(type: "int", nullable: false),
                    CompletionRate = table.Column<double>(type: "float", nullable: false),
                    AverageScore = table.Column<double>(type: "float", nullable: false),
                    TotalTimeSpent = table.Column<int>(type: "int", nullable: false),
                    AverageTimePerQuiz = table.Column<double>(type: "float", nullable: false),
                    FirstAttemptAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastAttemptAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentPerformanceSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentPerformanceSummaries_Links_LinkId",
                        column: x => x.LinkId,
                        principalTable: "Links",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentPerformanceSummaries_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentQuizPerformances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmittedAnswerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    PointsEarned = table.Column<int>(type: "int", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeSpentSeconds = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttemptsCount = table.Column<int>(type: "int", nullable: false),
                    LastAttemptAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentQuizPerformances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentQuizPerformances_Links_LinkId",
                        column: x => x.LinkId,
                        principalTable: "Links",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentQuizPerformances_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentQuizPerformances_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentQuizSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastActivityAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SessionToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentAnswerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeSpentSeconds = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentQuizSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentQuizSessions_Links_LinkId",
                        column: x => x.LinkId,
                        principalTable: "Links",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentQuizSessions_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentQuizSessions_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 3, 18, 30, 29, 754, DateTimeKind.Utc).AddTicks(5910), "$2a$12$SRmnlJe5CZ09ekcY27nfe.Wjt47s1c/if/8kEIVBqSDqZ6d.tZ40C" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentPerformanceSummaries_LinkId",
                table: "StudentPerformanceSummaries",
                column: "LinkId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPerformanceSummaries_StudentId",
                table: "StudentPerformanceSummaries",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizPerformances_LinkId",
                table: "StudentQuizPerformances",
                column: "LinkId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizPerformances_QuizId",
                table: "StudentQuizPerformances",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizPerformances_StudentId",
                table: "StudentQuizPerformances",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizSessions_LinkId",
                table: "StudentQuizSessions",
                column: "LinkId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizSessions_QuizId",
                table: "StudentQuizSessions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizSessions_StudentId",
                table: "StudentQuizSessions",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentPerformanceSummaries");

            migrationBuilder.DropTable(
                name: "StudentQuizPerformances");

            migrationBuilder.DropTable(
                name: "StudentQuizSessions");

            migrationBuilder.CreateTable(
                name: "StudentQuizAttempts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    PointsEarned = table.Column<int>(type: "int", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmittedAnswerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeSpentSeconds = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentQuizAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentQuizAttempts_Links_LinkId",
                        column: x => x.LinkId,
                        principalTable: "Links",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentQuizAttempts_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentQuizAttempts_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 1, 19, 12, 56, 611, DateTimeKind.Utc).AddTicks(4490), "$2a$12$2vtEwPpF4oak0WJtHF7y3.6.//.hr6d027bZZIPYYOrz.OSuAXpQ." });

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizAttempts_LinkId",
                table: "StudentQuizAttempts",
                column: "LinkId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizAttempts_QuizId",
                table: "StudentQuizAttempts",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizAttempts_StudentId",
                table: "StudentQuizAttempts",
                column: "StudentId");
        }
    }
}
