using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PlanId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RegistrationPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistrationPlanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    StripeProductName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationPlans", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RegistrationPlans",
                columns: new[] { "Id", "Price", "RegistrationPlanName", "StripeProductName", "Type" },
                values: new object[,]
                {
                    { new Guid("a1a1a1a1-a1a1-1111-1111-111111111111"), 2000L, "Bazë", "Student - Monthly Bazë", "monthly" },
                    { new Guid("a1a1a1a1-a1a1-2222-2222-222222222222"), 4000L, "Standarde", "Student - Monthly Standarde", "monthly" },
                    { new Guid("a1a1a1a1-a1a1-3333-3333-333333333333"), 6000L, "Premium", "Student - Monthly Premium", "monthly" },
                    { new Guid("a1a1a1a1-a1a1-4444-4444-444444444444"), 20000L, "Bazë", "Student - Yearly Bazë", "yearly" },
                    { new Guid("a1a1a1a1-a1a1-5555-5555-555555555555"), 40000L, "Standarde", "Student - Yearly Standarde", "yearly" },
                    { new Guid("a1a1a1a1-a1a1-6666-6666-666666666666"), 60000L, "Premium", "Student - Yearly Premium", "yearly" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "$2a$12$frMhyUwoxnGT6AwRr/eyyuYXUgntjX796XUqkfks/iP68nHK1gfKW");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PlanId",
                table: "Payments",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_RegistrationPlans_PlanId",
                table: "Payments",
                column: "PlanId",
                principalTable: "RegistrationPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_RegistrationPlans_PlanId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "RegistrationPlans");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PlanId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Payments");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "$2a$12$WjaJMZiTRR8wBYSChLvFne.lSXwHgwfv81Wb6C51Aj7FKBv4i/Ng.");
        }
    }
}
