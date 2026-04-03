using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddExplanationImageIdToQuizz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExplanationImageId",
                table: "Quizzes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4700), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4700), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4710), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4710), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4720), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4730), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4750), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4760), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4760) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4770), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4770) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4780), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4780) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4790), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4790) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4910), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4910), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4920), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4920) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4920), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4920) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4930), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4930) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4930), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4930) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4940), new DateTime(2026, 3, 25, 18, 16, 55, 775, DateTimeKind.Utc).AddTicks(4940) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 3, 25, 18, 16, 56, 130, DateTimeKind.Utc).AddTicks(9840), "$2a$12$foSfyKVg8SzBlmkBqXvJ4eLPADpay8e66xfg2LV8SBLJwTxajrtxq" });

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_ExplanationImageId",
                table: "Quizzes",
                column: "ExplanationImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Files_ExplanationImageId",
                table: "Quizzes",
                column: "ExplanationImageId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Files_ExplanationImageId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_ExplanationImageId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "ExplanationImageId",
                table: "Quizzes");

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(8930), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(8930) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(8950), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(8950) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(8960), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(8960) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9030) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9060), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9060) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9070), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9070) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9140), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9140) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9140), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9140) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9150), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9150) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9160), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9160) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9170), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9170) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9180), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9180) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9200), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9200) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9200), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9200) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9210), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9210) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9220), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9220) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9220), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9220) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9230), new DateTime(2025, 10, 11, 16, 17, 9, 822, DateTimeKind.Utc).AddTicks(9230) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 10, 11, 16, 17, 10, 181, DateTimeKind.Utc).AddTicks(6080), "$2a$12$qwph3VXS/FWgLaZxgNAvSuUk4033e5b/gWlnkMElqTeuD28w1rG3y" });
        }
    }
}
