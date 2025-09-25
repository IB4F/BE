using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLearnHubToTierBased : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearnHubs_SubscriptionPackages_SubscriptionPackageId",
                table: "LearnHubs");

            migrationBuilder.DropIndex(
                name: "IX_LearnHubs_SubscriptionPackageId",
                table: "LearnHubs");

            migrationBuilder.DropColumn(
                name: "SubscriptionPackageId",
                table: "LearnHubs");

            migrationBuilder.AddColumn<string>(
                name: "RequiredTier",
                table: "LearnHubs",
                type: "nvarchar(max)",
                nullable: true);

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
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 25, 13, 18, 5, 692, DateTimeKind.Utc).AddTicks(3590), "$2a$12$tao5gqZTRNYV0Yajxsyqburu8y0noZOQIwyd8BcjshxRzcxsSBV7y" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredTier",
                table: "LearnHubs");

            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionPackageId",
                table: "LearnHubs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3720), new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3720) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3730), new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3730) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3740), new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3740) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3750), new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3750) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3780), new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3780) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3780), new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3780) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3830), new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3830) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3840), new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3840) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3850), new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3850) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3860), new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3860) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3870), new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3870) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3880), new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3880) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3890), new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3890) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3900), new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3900) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3900), new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3910) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3910), new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3910) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3920), new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3920) });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3930), new DateTime(2025, 9, 25, 11, 52, 38, 318, DateTimeKind.Utc).AddTicks(3930) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreateAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 9, 25, 11, 52, 38, 850, DateTimeKind.Utc).AddTicks(9090), "$2a$12$pgSj740vlijplJDX86Ocz.UAlJmnYnM3NEscn3RKSid/8AU6v145y" });

            migrationBuilder.CreateIndex(
                name: "IX_LearnHubs_SubscriptionPackageId",
                table: "LearnHubs",
                column: "SubscriptionPackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_LearnHubs_SubscriptionPackages_SubscriptionPackageId",
                table: "LearnHubs",
                column: "SubscriptionPackageId",
                principalTable: "SubscriptionPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
