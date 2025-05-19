using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeachingBACKEND.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LearnHubs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnHubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    EmailVerificationToken = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmailVerificationTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CurrentClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    School = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profession = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordResetToken = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PasswordResetTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefreshToken = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Progress = table.Column<double>(type: "float", nullable: false),
                    LearnHubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Links_LearnHubs_LearnHubId",
                        column: x => x.LearnHubId,
                        principalTable: "LearnHubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StripeSessionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StripePaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Options = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAnswered = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quizzes_Links_LinkId",
                        column: x => x.LinkId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("12f4c32b-4a42-4b1f-9247-9b40efb21363"), "Fier" },
                    { new Guid("2dff0b99-3d86-47a5-b7ad-4e6d3c0ec748"), "Elbasan" },
                    { new Guid("69e02c84-4a61-406e-844d-24df2e25a983"), "Gjirokastër" },
                    { new Guid("8e065f0d-71c1-4a63-804b-49b1d08c1407"), "Berat" },
                    { new Guid("a2d4a4ee-5fa2-4a33-bd3c-2bbf98e9310b"), "Tirana" },
                    { new Guid("b8e13e5a-bba4-48b6-99d6-c4f123ab2cb3"), "Durrës" },
                    { new Guid("bc4e14b5-7d7e-4b6c-8b33-eaa2c91f9015"), "Vlorë" },
                    { new Guid("d05f0e99-20f0-4c9a-b03f-3ea92ec02b41"), "Shkodër" },
                    { new Guid("d28f0be5-5f17-4ec8-b365-7387c22234e9"), "Lezhë" },
                    { new Guid("e5f957d6-7a31-45b2-bded-44e4992d4b83"), "Kukës" },
                    { new Guid("f8d8e184-74c0-4551-b66b-7ae3f05e7ff5"), "Korçë" }
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("013d0df5-50ef-4269-8a12-9b4f91ef07e1"), "Klasa 12" },
                    { new Guid("10840373-f0f4-4b10-9ee0-c5a831b6cf6a"), "Klasa 5" },
                    { new Guid("1c0b8bb7-9eb9-4a4d-8c4d-67de8057ae49"), "Klasa 10" },
                    { new Guid("3402fc90-d7be-420e-a980-2ff430d84838"), "Klasa 11" },
                    { new Guid("43e7b804-0e1a-4c82-9e44-6a194ee1ff63"), "Klasa 8" },
                    { new Guid("7fc8018d-6310-4c1f-b878-4b3a5b0b265c"), "Klasa 6" },
                    { new Guid("81bce1db-4f7c-4f6f-9e59-cde56a8200b6"), "Klasa 3" },
                    { new Guid("8e77a87f-e42b-487a-9cde-54f648c8c457"), "Klasa 7" },
                    { new Guid("a61d58b7-23f8-48f7-9778-3e048c5808a0"), "Klasa 2" },
                    { new Guid("e3f9a8f1-9c4e-4a91-8bcb-0b6b1583d3a1"), "Klasa 1" },
                    { new Guid("f82a2ea4-c4c9-4895-9484-0197a299c02f"), "Klasa 4" },
                    { new Guid("fd4b14ea-1b79-4e4c-83e0-0196c55b4bc1"), "Klasa 9" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ApprovalStatus", "City", "CurrentClass", "DateOfBirth", "Email", "EmailVerificationToken", "EmailVerificationTokenExpiry", "FirstName", "IsEmailVerified", "LastName", "PasswordHash", "PasswordResetToken", "PasswordResetTokenExpiry", "PhoneNumber", "PostalCode", "Profession", "RefreshToken", "RefreshTokenExpiry", "Role", "School" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), 1, "Tirana", null, new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@teachapp.com", null, null, "System", true, "Administrator", "$2a$12$NGdgHR9QTdScxYW/BWASH.VG3wNEu9.c0U3KWSWSHIx5IUBFNbrYG", null, null, "+35500000000", null, "Administrator", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Main Admin Office" });

            migrationBuilder.CreateIndex(
                name: "IX_Links_LearnHubId",
                table: "Links",
                column: "LearnHubId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_LinkId",
                table: "Quizzes",
                column: "LinkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "LearnHubs");
        }
    }
}
