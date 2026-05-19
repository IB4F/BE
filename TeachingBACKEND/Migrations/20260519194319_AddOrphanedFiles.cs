using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddOrphanedFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "PenaltyPoints",
                table: "StudentPerformanceSummaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "QuestionImageId",
                table: "Quizzes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DragMatchPayloads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizzId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DragMatchPayloads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DragMatchPayloads_Quizzes_QuizzId",
                        column: x => x.QuizzId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DragOrderPayloads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizzId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorrectOrder = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DragOrderPayloads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DragOrderPayloads_Quizzes_QuizzId",
                        column: x => x.QuizzId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DragSpellPayloads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizzId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Letters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DragSpellPayloads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DragSpellPayloads_Files_ImageFileId",
                        column: x => x.ImageFileId,
                        principalTable: "Files",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DragSpellPayloads_Quizzes_QuizzId",
                        column: x => x.QuizzId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrphanedFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrphanedFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentQuizResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        name: "FK_StudentQuizResults_Links_LinkId",
                        column: x => x.LinkId,
                        principalTable: "Links",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentQuizResults_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentQuizResults_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DragMatchPairs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DragMatchPayloadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DragMatchPairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DragMatchPairs_DragMatchPayloads_DragMatchPayloadId",
                        column: x => x.DragMatchPayloadId,
                        principalTable: "DragMatchPayloads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DragMatchPairs_Files_ImageFileId",
                        column: x => x.ImageFileId,
                        principalTable: "Files",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DragOrderTiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DragOrderPayloadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DragOrderTiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DragOrderTiles_DragOrderPayloads_DragOrderPayloadId",
                        column: x => x.DragOrderPayloadId,
                        principalTable: "DragOrderPayloads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "QuizTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("c1c1c1c1-c1c1-1111-1111-111111111111"), "DragSpell" },
                    { new Guid("c2c2c2c2-c2c2-2222-2222-222222222222"), "DragOrder" },
                    { new Guid("c3c3c3c3-c3c3-3333-3333-333333333333"), "DragMatch" }
                });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1780), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1780) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1790), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1790) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1790), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1790) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1800), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1800) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1810), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1810) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1810), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1810) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1850), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1850) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1860), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1860) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1870), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1870) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1870), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1870) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1880), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1880) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1880), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1880) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1890), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1890) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1890), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1890) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1900), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1900) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1910), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1910) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1910), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1910) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1920), new DateTime(2026, 5, 19, 19, 43, 18, 8, DateTimeKind.Utc).AddTicks(1920) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 19, 19, 43, 18, 364, DateTimeKind.Utc).AddTicks(6580), "$2a$12$TGmbCNTKIqLamqSfy.LYLuhadkLUhUGqP9jDRlh6hvhtB2csVfJ/2" });

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_QuestionImageId",
                table: "Quizzes",
                column: "QuestionImageId");

            migrationBuilder.CreateIndex(
                name: "IX_DragMatchPairs_DragMatchPayloadId",
                table: "DragMatchPairs",
                column: "DragMatchPayloadId");

            migrationBuilder.CreateIndex(
                name: "IX_DragMatchPairs_ImageFileId",
                table: "DragMatchPairs",
                column: "ImageFileId");

            migrationBuilder.CreateIndex(
                name: "IX_DragMatchPayloads_QuizzId",
                table: "DragMatchPayloads",
                column: "QuizzId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DragOrderPayloads_QuizzId",
                table: "DragOrderPayloads",
                column: "QuizzId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DragOrderTiles_DragOrderPayloadId",
                table: "DragOrderTiles",
                column: "DragOrderPayloadId");

            migrationBuilder.CreateIndex(
                name: "IX_DragSpellPayloads_ImageFileId",
                table: "DragSpellPayloads",
                column: "ImageFileId");

            migrationBuilder.CreateIndex(
                name: "IX_DragSpellPayloads_QuizzId",
                table: "DragSpellPayloads",
                column: "QuizzId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizResults_LinkId",
                table: "StudentQuizResults",
                column: "LinkId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizResults_QuizId",
                table: "StudentQuizResults",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentQuizResults_StudentId_QuizId",
                table: "StudentQuizResults",
                columns: new[] { "StudentId", "QuizId" },
                unique: true);

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

            migrationBuilder.DropTable(
                name: "DragMatchPairs");

            migrationBuilder.DropTable(
                name: "DragOrderTiles");

            migrationBuilder.DropTable(
                name: "DragSpellPayloads");

            migrationBuilder.DropTable(
                name: "OrphanedFiles");

            migrationBuilder.DropTable(
                name: "StudentQuizResults");

            migrationBuilder.DropTable(
                name: "DragMatchPayloads");

            migrationBuilder.DropTable(
                name: "DragOrderPayloads");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_QuestionImageId",
                table: "Quizzes");

            migrationBuilder.DeleteData(
                table: "QuizTypes",
                keyColumn: "Id",
                keyValue: new Guid("c1c1c1c1-c1c1-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "QuizTypes",
                keyColumn: "Id",
                keyValue: new Guid("c2c2c2c2-c2c2-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "QuizTypes",
                keyColumn: "Id",
                keyValue: new Guid("c3c3c3c3-c3c3-3333-3333-333333333333"));

            migrationBuilder.DropColumn(
                name: "CorrectAnswerQuiz",
                table: "StudentPerformanceSummaries");

            migrationBuilder.DropColumn(
                name: "LastCompletedQuizId",
                table: "StudentPerformanceSummaries");

            migrationBuilder.DropColumn(
                name: "PenaltyPoints",
                table: "StudentPerformanceSummaries");

            migrationBuilder.DropColumn(
                name: "QuestionImageId",
                table: "Quizzes");

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2630), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2630) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2640), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2640) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2640), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2640) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2650), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2650) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2660), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2660) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2670), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2700), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2710), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2710), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2720), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2720), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2730), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2730), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2740) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2740), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2740) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2740), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2740) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2750), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2750), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2760), new DateTime(2026, 4, 24, 20, 59, 20, 135, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 4, 24, 20, 59, 20, 497, DateTimeKind.Utc).AddTicks(4490), "$2a$12$OxRL9/6NcNGzk.UIMVct1.468ypYe6Hepuu0c9HShvwtGSEbWUajC" });
        }
    }
}
