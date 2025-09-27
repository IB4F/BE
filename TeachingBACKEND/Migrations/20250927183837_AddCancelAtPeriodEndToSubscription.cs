using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class AddCancelAtPeriodEndToSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions");

            migrationBuilder.AddColumn<bool>(
                name: "CancelAtPeriodEnd",
                table: "Subscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6280), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6280) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6290), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6290) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6290), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6290) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6300), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6300) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6310), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6310) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6320), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6320) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6350), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6350) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6350), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6350) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6360), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6360) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6370), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6370) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6370), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6370) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6380), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6380) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6400), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6400), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6410) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6410), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6410) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6410), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6410) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 9, 27, 18, 38, 35, 727, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "Email", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 27, 18, 38, 36, 79, DateTimeKind.Utc).AddTicks(4670), "admin@braingainalbania.al", "$2a$12$H5Fhr2HdxFuC/3aIH0t91ePJntHKz/KFrPhMYrzA/hfLbXMRkZDJK" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ParentUserId",
                table: "Users",
                column: "ParentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_Status",
                table: "Subscriptions",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_Status_CurrentPeriodEnd",
                table: "Subscriptions",
                columns: new[] { "Status", "CurrentPeriodEnd" });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_StripeSubscriptionId",
                table: "Subscriptions",
                column: "StripeSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_UserId_Status",
                table: "Subscriptions",
                columns: new[] { "UserId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPayments_PaidAt",
                table: "SubscriptionPayments",
                column: "PaidAt");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPayments_Status",
                table: "SubscriptionPayments",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_ParentUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_Status",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_Status_CurrentPeriodEnd",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_StripeSubscriptionId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_UserId_Status",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPayments_PaidAt",
                table: "SubscriptionPayments");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPayments_Status",
                table: "SubscriptionPayments");

            migrationBuilder.DropColumn(
                name: "CancelAtPeriodEnd",
                table: "Subscriptions");

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(2880), new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(2880) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(2880), new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(2880) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(2890), new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(2890) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(2890), new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(2890) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(2910), new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(2910) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(2920), new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(2920) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(2970) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(2980), new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(2980) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(2990), new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(2990) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(2990), new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(2990) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(3000), new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(3000) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(3030), new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(3030) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(3030), new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(3030) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(3040) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(3040) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 9, 25, 13, 18, 5, 322, DateTimeKind.Utc).AddTicks(3060) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "Email", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 25, 13, 18, 5, 692, DateTimeKind.Utc).AddTicks(3590), "admin@teachapp.com", "$2a$12$tao5gqZTRNYV0Yajxsyqburu8y0noZOQIwyd8BcjshxRzcxsSBV7y" });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions",
                column: "UserId");
        }
    }
}
