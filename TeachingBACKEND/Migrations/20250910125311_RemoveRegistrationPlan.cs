using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRegistrationPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_RegistrationPlans_PlanId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_SubscriptionPackages_SubscriptionPackageId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_RegistrationPlans_PlanId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_SubscriptionPackages_SubscriptionPackageId",
                table: "Subscriptions");

            migrationBuilder.DropTable(
                name: "RegistrationPlans");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PlanId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_PlanId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Subscriptions");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubscriptionPackageId",
                table: "Subscriptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);


            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2370), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2370) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2370), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2370) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2380), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2380) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2380), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2380) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2400) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2400) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2420), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2420) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2530), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2530) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2540), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2540) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2540), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2540) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2550), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2550) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2560), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2560) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2560), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2560) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2560), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2560) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2570), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2570) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2570), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2570) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2580), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2580) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2580), new DateTime(2025, 9, 10, 12, 53, 8, 788, DateTimeKind.Utc).AddTicks(2580) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 10, 12, 53, 9, 204, DateTimeKind.Utc).AddTicks(6520), "$2a$12$SHHSi0YUlTNR9gblE0EdiuzZnp8fQyhgZ891.BknEZKWXUcefbg/G" });

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_SubscriptionPackages_SubscriptionPackageId",
                table: "Payments",
                column: "SubscriptionPackageId",
                principalTable: "SubscriptionPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_SubscriptionPackages_SubscriptionPackageId",
                table: "Subscriptions",
                column: "SubscriptionPackageId",
                principalTable: "SubscriptionPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_SubscriptionPackages_SubscriptionPackageId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_SubscriptionPackages_SubscriptionPackageId",
                table: "Subscriptions");

            migrationBuilder.AddColumn<Guid>(
                name: "PlanId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanId",
                table: "Subscriptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "SubscriptionPackageId",
                table: "Subscriptions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "PlanId",
                table: "Subscriptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "RegistrationPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllowCancellation = table.Column<bool>(type: "bit", nullable: false),
                    IsFamilyPlan = table.Column<bool>(type: "bit", nullable: false),
                    IsSubscription = table.Column<bool>(type: "bit", nullable: false),
                    MaxUsers = table.Column<int>(type: "int", nullable: false),
                    MonthlyPrice = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    RegistrationPlanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StripeMonthlyPriceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StripeProductId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StripeProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StripeYearlyPriceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrialDays = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearlyPrice = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationPlans", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RegistrationPlans",
                columns: new[] { "Id", "AllowCancellation", "IsFamilyPlan", "IsSubscription", "MaxUsers", "MonthlyPrice", "Price", "RegistrationPlanName", "StripeMonthlyPriceId", "StripeProductId", "StripeProductName", "StripeYearlyPriceId", "TrialDays", "Type", "UserType", "YearlyPrice" },
                values: new object[,]
                {
                    { new Guid("a1a1a1a1-a1a1-1111-1111-111111111111"), true, false, true, 1, 2000L, 2000L, "Bazë", "price_monthly_baze", "prod_student_baze", "Student - Monthly Bazë", "price_yearly_baze", 7, "monthly", "student", 20000L },
                    { new Guid("a1a1a1a1-a1a1-2222-2222-222222222222"), true, false, true, 1, 4000L, 4000L, "Standarde", "price_monthly_standarde", "prod_student_standarde", "Student - Monthly Standarde", "price_yearly_standarde", 7, "monthly", "student", 40000L },
                    { new Guid("a1a1a1a1-a1a1-3333-3333-333333333333"), true, false, true, 1, 6000L, 6000L, "Premium", "price_monthly_premium", "prod_student_premium", "Student - Monthly Premium", "price_yearly_premium", 7, "monthly", "student", 60000L },
                    { new Guid("a1a1a1a1-a1a1-4444-4444-444444444444"), true, false, true, 1, 2000L, 20000L, "Bazë", "price_monthly_baze", "prod_student_baze", "Student - Yearly Bazë", "price_yearly_baze", 7, "yearly", "student", 20000L },
                    { new Guid("a1a1a1a1-a1a1-5555-5555-555555555555"), true, true, true, 5, 4000L, 40000L, "Standarde", "price_monthly_standarde", "prod_family_standarde", "Student - Yearly Standarde", "price_yearly_standarde", 7, "yearly", "family", 40000L },
                    { new Guid("a1a1a1a1-a1a1-6666-6666-666666666666"), true, true, true, 10, 6000L, 60000L, "Premium", "price_monthly_premium", "prod_family_premium", "Student - Yearly Premium", "price_yearly_premium", 7, "yearly", "family", 60000L },
                    { new Guid("a1a1a1a1-a1a1-7777-7777-777777777777"), true, false, true, 50, 10000L, 10000L, "Supervisor - Monthly", "price_supervisor_monthly", "prod_supervisor", "Supervisor - Monthly Plan", "price_supervisor_yearly", 14, "monthly", "supervisor", 100000L },
                    { new Guid("a1a1a1a1-a1a1-8888-8888-888888888888"), true, false, true, 500, 10000L, 100000L, "Supervisor - Yearly", "price_supervisor_monthly", "prod_supervisor", "Supervisor - Yearly Plan", "price_supervisor_yearly", 14, "yearly", "supervisor", 100000L }
                });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8310), new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8310) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8320), new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8320) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8330), new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8330) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8340), new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8340) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8360), new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8360) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8370), new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8370) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8440), new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8440) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8460), new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8480), new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8490) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8490), new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8500) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8500), new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8510) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8520), new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8520) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8530), new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8530) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 9, 14, 49, 5, 773, DateTimeKind.Utc).AddTicks(40), "$2a$12$O/rz0tJ.9.M06ly5UBXxhuLYh7jSkbtBS0fOPFDYWYsCd1M9M0zmu" });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PlanId",
                table: "Payments",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PlanId",
                table: "Subscriptions",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_RegistrationPlans_PlanId",
                table: "Payments",
                column: "PlanId",
                principalTable: "RegistrationPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_SubscriptionPackages_SubscriptionPackageId",
                table: "Payments",
                column: "SubscriptionPackageId",
                principalTable: "SubscriptionPackages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_RegistrationPlans_PlanId",
                table: "Subscriptions",
                column: "PlanId",
                principalTable: "RegistrationPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_SubscriptionPackages_SubscriptionPackageId",
                table: "Subscriptions",
                column: "SubscriptionPackageId",
                principalTable: "SubscriptionPackages",
                principalColumn: "Id");
        }
    }
}
