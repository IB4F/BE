using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddSpacedRepetitionTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ConceptTagId",
                table: "Quizzes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConceptTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserConceptMastery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConceptTagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MasteryLevel = table.Column<float>(type: "real", nullable: false),
                    TotalAttempts = table.Column<int>(type: "int", nullable: false),
                    CorrectAttempts = table.Column<int>(type: "int", nullable: false),
                    NextReviewDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastAttemptAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConceptMastery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserConceptMastery_ConceptTags_ConceptTagId",
                        column: x => x.ConceptTagId,
                        principalTable: "ConceptTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserConceptMastery_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(340), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(340) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(340), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(340) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(350), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(350) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(350), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(360) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(360), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(360) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(370), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(370) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(390), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(390) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(400), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(400) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(400), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(400) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(410), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(410) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(420), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(420) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(420), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(420) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(430), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(430) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(430), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(430) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(440), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(440) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(460), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(460) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(460), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(460) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(470), new DateTime(2026, 5, 31, 11, 57, 47, 634, DateTimeKind.Utc).AddTicks(470) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 31, 11, 57, 47, 999, DateTimeKind.Utc).AddTicks(5700), "$2a$12$cx0EFaWd1br6NciVXt7AHOsHKw5jolpRB8marijGPv/A6T.67tvS." });

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_ConceptTagId",
                table: "Quizzes",
                column: "ConceptTagId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConceptMastery_ConceptTagId",
                table: "UserConceptMastery",
                column: "ConceptTagId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConceptMastery_UserId_ConceptTagId",
                table: "UserConceptMastery",
                columns: new[] { "UserId", "ConceptTagId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_ConceptTags_ConceptTagId",
                table: "Quizzes",
                column: "ConceptTagId",
                principalTable: "ConceptTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_ConceptTags_ConceptTagId",
                table: "Quizzes");

            migrationBuilder.DropTable(
                name: "UserConceptMastery");

            migrationBuilder.DropTable(
                name: "ConceptTags");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_ConceptTagId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "ConceptTagId",
                table: "Quizzes");

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8410), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8440), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8440) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8440), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8440) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8450), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8460), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8460), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8500), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8500) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8510), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8510) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8510), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8510) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8520), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8520) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8520), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8530) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8530), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8530) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8530), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8540) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8540), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8540) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8540), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8540) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8550), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8550) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8560), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8560), new DateTime(2026, 5, 29, 15, 43, 25, 555, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 29, 15, 43, 25, 912, DateTimeKind.Utc).AddTicks(8740), "$2a$12$xwwB/LaXoE0y38MkJrHMa..ErL8nl50dZAOld6unnoJuFbS0WFnPq" });
        }
    }
}
