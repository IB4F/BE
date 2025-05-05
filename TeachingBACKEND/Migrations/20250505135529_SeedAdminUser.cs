using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ApprovalStatus", "City", "CurrentClass", "DateOfBirth", "Email", "EmailVerificationToken", "EmailVerificationTokenExpiry", "FirstName", "IsEmailVerified", "LastName", "PasswordHash", "PasswordResetToken", "PasswordResetTokenExpiry", "PhoneNumber", "PostalCode", "Profession", "RefreshToken", "RefreshTokenExpiry", "Role", "School" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), 1, "Tirana", null, new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@teachapp.com", null, null, "System", true, "Administrator", "$2a$12$djxmyWl0Ry4HPxw67L1yFO6I939zw7/yp1Gw8FDfS7/3tu.KKDlJm", null, null, "+35500000000", null, "Administrator", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Main Admin Office" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));
        }
    }
}
