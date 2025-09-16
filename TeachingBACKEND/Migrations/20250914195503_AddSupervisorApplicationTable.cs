using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddSupervisorApplicationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupervisorApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ContactPersonFirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactPersonLastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactPersonEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ContactPersonPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    EstimatedStudentCount = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TemporaryPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisorApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupervisorApplications_Users_ApprovedUserId",
                        column: x => x.ApprovedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1730), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1730) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1740), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1740) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1750), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1750) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1750), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1750) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1770), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1770) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1770), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1770) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1800), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1800) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1810), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1810) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1810), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1810) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1820), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1820) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1830), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1830) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1830), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1830) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1850), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1850) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1850), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1850) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1860), new DateTime(2025, 9, 14, 19, 55, 2, 384, DateTimeKind.Utc).AddTicks(1860) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 55, 3, 1, DateTimeKind.Utc).AddTicks(4810), "$2a$12$AmTRzxRSAASBswRuWvcvwurPs6y5w7Jb2MRIxzQDgsNixDLuxgyFW" });

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorApplications_ApplicationDate",
                table: "SupervisorApplications",
                column: "ApplicationDate");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorApplications_ApprovalStatus",
                table: "SupervisorApplications",
                column: "ApprovalStatus");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorApplications_ApprovedUserId",
                table: "SupervisorApplications",
                column: "ApprovedUserId",
                unique: true,
                filter: "[ApprovedUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorApplications_ContactPersonEmail",
                table: "SupervisorApplications",
                column: "ContactPersonEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupervisorApplications");

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1670) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1680) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1680) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1690) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1710) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1710) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1730), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1740) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1740), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1740) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1750), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1750) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1760), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1760) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1760), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1760) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1770), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1770) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1780), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1780) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1780), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1780) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1800), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1800) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1800), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1800) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1810), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1810) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1810), new DateTime(2025, 9, 14, 19, 50, 46, 342, DateTimeKind.Utc).AddTicks(1810) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 14, 19, 50, 46, 782, DateTimeKind.Utc).AddTicks(8680), "$2a$12$X/Mc0Ym2X0LuJ5WgReGGs.2Ig4aiAcnALODjdmb62Dq4.xAqVqEii" });
        }
    }
}
