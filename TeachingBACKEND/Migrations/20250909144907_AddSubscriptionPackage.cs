using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscriptionPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionPackageId",
                table: "Subscriptions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionPackageId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubscriptionPackages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    Tier = table.Column<int>(type: "int", nullable: false),
                    BillingInterval = table.Column<int>(type: "int", nullable: false),
                    MonthlyPrice = table.Column<long>(type: "bigint", nullable: false),
                    YearlyPrice = table.Column<long>(type: "bigint", nullable: false),
                    BasePrice = table.Column<long>(type: "bigint", nullable: true),
                    PricePerAdditionalMember = table.Column<long>(type: "bigint", nullable: true),
                    MinFamilyMembers = table.Column<int>(type: "int", nullable: true),
                    MaxFamilyMembers = table.Column<int>(type: "int", nullable: true),
                    StripeMonthlyPriceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StripeYearlyPriceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxUsers = table.Column<int>(type: "int", nullable: false),
                    TrialDays = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPackages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPackages",
                columns: new[] { "Id", "BasePrice", "BillingInterval", "CreatedAt", "Description", "IsActive", "MaxFamilyMembers", "MaxUsers", "MinFamilyMembers", "MonthlyPrice", "Name", "PricePerAdditionalMember", "StripeMonthlyPriceId", "StripeYearlyPriceId", "Tier", "TrialDays", "UpdatedAt", "UserType", "YearlyPrice" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, 3, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8310), "Basic student package with monthly billing", true, null, 1, null, 2000L, "Student Basic Monthly", null, "price_student_basic_monthly", "price_student_basic_yearly", 1, 7, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8310), 1, 20000L },
                    { new Guid("11111111-1111-2222-2222-222222222222"), null, 4, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8320), "Basic student package with yearly billing", true, null, 1, null, 2000L, "Student Basic Yearly", null, "price_student_basic_monthly", "price_student_basic_yearly", 1, 7, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8320), 1, 20000L },
                    { new Guid("22222222-2222-1111-1111-111111111111"), null, 3, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8330), "Standard student package with monthly billing", true, null, 1, null, 4000L, "Student Standard Monthly", null, "price_student_standard_monthly", "price_student_standard_yearly", 2, 7, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8330), 1, 40000L },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, 4, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8340), "Standard student package with yearly billing", true, null, 1, null, 4000L, "Student Standard Yearly", null, "price_student_standard_monthly", "price_student_standard_yearly", 2, 7, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8340), 1, 40000L },
                    { new Guid("33333333-3333-1111-1111-111111111111"), null, 3, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8360), "Premium student package with monthly billing", true, null, 1, null, 6000L, "Student Premium Monthly", null, "price_student_premium_monthly", "price_student_premium_yearly", 3, 7, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8360), 1, 60000L },
                    { new Guid("33333333-3333-2222-2222-222222222222"), null, 4, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8370), "Premium student package with yearly billing", true, null, 1, null, 6000L, "Student Premium Yearly", null, "price_student_premium_monthly", "price_student_premium_yearly", 3, 7, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8370), 1, 60000L },
                    { new Guid("44444444-4444-1111-1111-111111111111"), 3000L, 3, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8390), "Basic family package with monthly billing - dynamic pricing based on family size", true, 5, 5, 1, 3000L, "Family Basic Monthly", 1000L, "price_family_basic_monthly", "price_family_basic_yearly", 1, 7, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8390), 2, 30000L },
                    { new Guid("44444444-4444-2222-2222-222222222222"), 30000L, 4, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8400), "Basic family package with yearly billing - dynamic pricing based on family size", true, 5, 5, 1, 3000L, "Family Basic Yearly", 2000L, "price_family_basic_monthly", "price_family_basic_yearly", 1, 7, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8410), 2, 30000L },
                    { new Guid("55555555-5555-1111-1111-111111111111"), 5000L, 3, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8410), "Standard family package with monthly billing - dynamic pricing based on family size", true, 10, 10, 1, 5000L, "Family Standard Monthly", 1000L, "price_family_standard_monthly", "price_family_standard_yearly", 2, 7, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8420), 2, 50000L },
                    { new Guid("55555555-5555-2222-2222-222222222222"), 50000L, 4, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8420), "Standard family package with yearly billing - dynamic pricing based on family size", true, 10, 10, 1, 5000L, "Family Standard Yearly", 2000L, "price_family_standard_monthly", "price_family_standard_yearly", 2, 7, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8430), 2, 50000L },
                    { new Guid("66666666-6666-1111-1111-111111111111"), 8000L, 3, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8440), "Premium family package with monthly billing - dynamic pricing based on family size", true, 15, 15, 1, 8000L, "Family Premium Monthly", 1000L, "price_family_premium_monthly", "price_family_premium_yearly", 3, 7, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8440), 2, 80000L },
                    { new Guid("66666666-6666-2222-2222-222222222222"), 80000L, 4, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8450), "Premium family package with yearly billing - dynamic pricing based on family size", true, 15, 15, 1, 8000L, "Family Premium Yearly", 2000L, "price_family_premium_monthly", "price_family_premium_yearly", 3, 7, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8450), 2, 80000L },
                    { new Guid("77777777-7777-1111-1111-111111111111"), null, 3, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8460), "Basic supervisor package with monthly billing", true, null, 50, null, 10000L, "Supervisor Basic Monthly", null, "price_supervisor_basic_monthly", "price_supervisor_basic_yearly", 1, 14, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8460), 3, 100000L },
                    { new Guid("77777777-7777-2222-2222-222222222222"), null, 4, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8480), "Basic supervisor package with yearly billing", true, null, 50, null, 10000L, "Supervisor Basic Yearly", null, "price_supervisor_basic_monthly", "price_supervisor_basic_yearly", 1, 14, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8490), 3, 100000L },
                    { new Guid("88888888-8888-1111-1111-111111111111"), null, 3, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8490), "Standard supervisor package with monthly billing", true, null, 100, null, 20000L, "Supervisor Standard Monthly", null, "price_supervisor_standard_monthly", "price_supervisor_standard_yearly", 2, 14, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8500), 3, 200000L },
                    { new Guid("88888888-8888-2222-2222-222222222222"), null, 4, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8500), "Standard supervisor package with yearly billing", true, null, 100, null, 20000L, "Supervisor Standard Yearly", null, "price_supervisor_standard_monthly", "price_supervisor_standard_yearly", 2, 14, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8510), 3, 200000L },
                    { new Guid("99999999-9999-1111-1111-111111111111"), null, 3, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8520), "Premium supervisor package with monthly billing", true, null, 500, null, 30000L, "Supervisor Premium Monthly", null, "price_supervisor_premium_monthly", "price_supervisor_premium_yearly", 3, 14, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8520), 3, 300000L },
                    { new Guid("99999999-9999-2222-2222-222222222222"), null, 4, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8530), "Premium supervisor package with yearly billing", true, null, 500, null, 30000L, "Supervisor Premium Yearly", null, "price_supervisor_premium_monthly", "price_supervisor_premium_yearly", 3, 14, new DateTime(2025, 9, 9, 14, 49, 5, 270, DateTimeKind.Utc).AddTicks(8530), 3, 300000L }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 9, 14, 49, 5, 773, DateTimeKind.Utc).AddTicks(40), "$2a$12$O/rz0tJ.9.M06ly5UBXxhuLYh7jSkbtBS0fOPFDYWYsCd1M9M0zmu" });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubscriptionPackageId",
                table: "Subscriptions",
                column: "SubscriptionPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_SubscriptionPackageId",
                table: "Payments",
                column: "SubscriptionPackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_SubscriptionPackages_SubscriptionPackageId",
                table: "Payments",
                column: "SubscriptionPackageId",
                principalTable: "SubscriptionPackages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_SubscriptionPackages_SubscriptionPackageId",
                table: "Subscriptions",
                column: "SubscriptionPackageId",
                principalTable: "SubscriptionPackages",
                principalColumn: "Id");
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

            migrationBuilder.DropTable(
                name: "SubscriptionPackages");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_SubscriptionPackageId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Payments_SubscriptionPackageId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "SubscriptionPackageId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionPackageId",
                table: "Payments");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 7, 11, 42, 2, 40, DateTimeKind.Utc).AddTicks(8640), "$2a$12$9s9szKHkRwZa.8N19J3J2e8HzHGGGKuTNnfiH3SkfnLycP764KZz2" });
        }
    }
}
