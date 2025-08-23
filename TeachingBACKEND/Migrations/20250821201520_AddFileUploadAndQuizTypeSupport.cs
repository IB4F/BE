using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddFileUploadAndQuizTypeSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioUrl",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Quizzes");

            migrationBuilder.AddColumn<Guid>(
                name: "ExplanationAudioId",
                table: "Quizzes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "QuestionAudioId",
                table: "Quizzes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "QuizzTypeId",
                table: "Quizzes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OptionImageId",
                table: "Options",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OriginalFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 21, 20, 15, 20, 122, DateTimeKind.Utc).AddTicks(3090), "$2a$12$lvdxp7dbPKD1Y0s4n59Sp.O9u8NvHYfdYMM6I7M50PPRCAmaydR5y" });

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_ExplanationAudioId",
                table: "Quizzes",
                column: "ExplanationAudioId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_QuestionAudioId",
                table: "Quizzes",
                column: "QuestionAudioId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_QuizzTypeId",
                table: "Quizzes",
                column: "QuizzTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_OptionImageId",
                table: "Options",
                column: "OptionImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Files_OptionImageId",
                table: "Options",
                column: "OptionImageId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Files_ExplanationAudioId",
                table: "Quizzes",
                column: "ExplanationAudioId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Files_QuestionAudioId",
                table: "Quizzes",
                column: "QuestionAudioId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_QuizTypes_QuizzTypeId",
                table: "Quizzes",
                column: "QuizzTypeId",
                principalTable: "QuizTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Files_OptionImageId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Files_ExplanationAudioId",
                table: "Quizzes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Files_QuestionAudioId",
                table: "Quizzes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_QuizTypes_QuizzTypeId",
                table: "Quizzes");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_ExplanationAudioId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_QuestionAudioId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_QuizzTypeId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Options_OptionImageId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "ExplanationAudioId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "QuestionAudioId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "QuizzTypeId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "OptionImageId",
                table: "Options");

            migrationBuilder.AddColumn<string>(
                name: "AudioUrl",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 21, 17, 18, 27, 9, DateTimeKind.Utc).AddTicks(9850), "$2a$12$Iisuhi873n9k09G6TGeHx.yuj8acLNdp7dudoDV4bT5Ag0cmmeBbW" });
        }
    }
}
