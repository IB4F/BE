using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscriptionSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ActiveSubscriptionId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ActiveSubscriptionId1",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionExpiresAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AllowCancellation",
                table: "RegistrationPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSubscription",
                table: "RegistrationPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "MonthlyPrice",
                table: "RegistrationPlans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "StripeMonthlyPriceId",
                table: "RegistrationPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StripeProductId",
                table: "RegistrationPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StripeYearlyPriceId",
                table: "RegistrationPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TrialDays",
                table: "RegistrationPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "YearlyPrice",
                table: "RegistrationPlans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StripeSubscriptionId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StripeCustomerId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StripePriceId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CanceledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CurrentPeriodStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CurrentPeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrialEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Interval = table.Column<int>(type: "int", nullable: false),
                    IntervalCount = table.Column<int>(type: "int", nullable: false),
                    RegistrationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_RegistrationPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "RegistrationPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StripePaymentIntentId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StripeInvoiceId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPayments_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-1111-1111-111111111111"),
                columns: new[] { "AllowCancellation", "IsSubscription", "MonthlyPrice", "StripeMonthlyPriceId", "StripeProductId", "StripeYearlyPriceId", "TrialDays", "YearlyPrice" },
                values: new object[] { true, true, 2000L, "price_monthly_baze", "prod_student_baze", "price_yearly_baze", 7, 20000L });

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-2222-2222-222222222222"),
                columns: new[] { "AllowCancellation", "IsSubscription", "MonthlyPrice", "StripeMonthlyPriceId", "StripeProductId", "StripeYearlyPriceId", "TrialDays", "YearlyPrice" },
                values: new object[] { true, true, 4000L, "price_monthly_standarde", "prod_student_standarde", "price_yearly_standarde", 7, 40000L });

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-3333-3333-333333333333"),
                columns: new[] { "AllowCancellation", "IsSubscription", "MonthlyPrice", "StripeMonthlyPriceId", "StripeProductId", "StripeYearlyPriceId", "TrialDays", "YearlyPrice" },
                values: new object[] { true, true, 6000L, "price_monthly_premium", "prod_student_premium", "price_yearly_premium", 7, 60000L });

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-4444-4444-444444444444"),
                columns: new[] { "AllowCancellation", "IsSubscription", "MonthlyPrice", "StripeMonthlyPriceId", "StripeProductId", "StripeYearlyPriceId", "TrialDays", "YearlyPrice" },
                values: new object[] { true, true, 2000L, "price_monthly_baze", "prod_student_baze", "price_yearly_baze", 7, 20000L });

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-5555-5555-555555555555"),
                columns: new[] { "AllowCancellation", "IsSubscription", "MonthlyPrice", "StripeMonthlyPriceId", "StripeProductId", "StripeYearlyPriceId", "TrialDays", "YearlyPrice" },
                values: new object[] { true, true, 4000L, "price_monthly_standarde", "prod_family_standarde", "price_yearly_standarde", 7, 40000L });

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-6666-6666-666666666666"),
                columns: new[] { "AllowCancellation", "IsSubscription", "MonthlyPrice", "StripeMonthlyPriceId", "StripeProductId", "StripeYearlyPriceId", "TrialDays", "YearlyPrice" },
                values: new object[] { true, true, 6000L, "price_monthly_premium", "prod_family_premium", "price_yearly_premium", 7, 60000L });

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-7777-7777-777777777777"),
                columns: new[] { "AllowCancellation", "IsSubscription", "MonthlyPrice", "StripeMonthlyPriceId", "StripeProductId", "StripeYearlyPriceId", "TrialDays", "YearlyPrice" },
                values: new object[] { true, true, 10000L, "price_supervisor_monthly", "prod_supervisor", "price_supervisor_yearly", 14, 100000L });

            migrationBuilder.UpdateData(
                table: "RegistrationPlans",
                keyColumn: "Id",
                keyValue: new Guid("a1a1a1a1-a1a1-8888-8888-888888888888"),
                columns: new[] { "AllowCancellation", "IsSubscription", "MonthlyPrice", "StripeMonthlyPriceId", "StripeProductId", "StripeYearlyPriceId", "TrialDays", "YearlyPrice" },
                values: new object[] { true, true, 10000L, "price_supervisor_monthly", "prod_supervisor", "price_supervisor_yearly", 14, 100000L });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ActiveSubscriptionId", "ActiveSubscriptionId1", "CreateAt", "PasswordHash", "SubscriptionExpiresAt" },
                values: new object[] { null, null, new DateTime(2025, 9, 7, 11, 42, 2, 40, DateTimeKind.Utc).AddTicks(8640), "$2a$12$9s9szKHkRwZa.8N19J3J2e8HzHGGGKuTNnfiH3SkfnLycP764KZz2", null });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ActiveSubscriptionId1",
                table: "Users",
                column: "ActiveSubscriptionId1");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPayments_SubscriptionId",
                table: "SubscriptionPayments",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PlanId",
                table: "Subscriptions",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Subscriptions_ActiveSubscriptionId1",
                table: "Users",
                column: "ActiveSubscriptionId1",
                principalTable: "Subscriptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Subscriptions_ActiveSubscriptionId1",
                table: "Users");

            migrationBuilder.DropTable(
                name: "SubscriptionPayments");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Users_ActiveSubscriptionId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ActiveSubscriptionId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ActiveSubscriptionId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SubscriptionExpiresAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AllowCancellation",
                table: "RegistrationPlans");

            migrationBuilder.DropColumn(
                name: "IsSubscription",
                table: "RegistrationPlans");

            migrationBuilder.DropColumn(
                name: "MonthlyPrice",
                table: "RegistrationPlans");

            migrationBuilder.DropColumn(
                name: "StripeMonthlyPriceId",
                table: "RegistrationPlans");

            migrationBuilder.DropColumn(
                name: "StripeProductId",
                table: "RegistrationPlans");

            migrationBuilder.DropColumn(
                name: "StripeYearlyPriceId",
                table: "RegistrationPlans");

            migrationBuilder.DropColumn(
                name: "TrialDays",
                table: "RegistrationPlans");

            migrationBuilder.DropColumn(
                name: "YearlyPrice",
                table: "RegistrationPlans");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 7, 11, 12, 7, 689, DateTimeKind.Utc).AddTicks(3420), "$2a$12$vfsxbdCnRpN0aIl6.AhGgO75hFwSwpoJNs0wtzCcPmp5atDJ6GHda" });
        }
    }
}
