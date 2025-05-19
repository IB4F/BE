using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachingBACKEND.Migrations
{
    public partial class ChangeIntToGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Links_LinkId1",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_LinkId1",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "LinkId1",
                table: "Quizzes");

            migrationBuilder.AlterColumn<Guid>(
                name: "LinkId",
                table: "Quizzes",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Quizzes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "$2a$12$4BIIyfxeYJpwpRkPWnHQfOPgHH3ntuRsIrjQwFPISzWrifceVLHuK");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_LinkId",
                table: "Quizzes",
                column: "LinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Links_LinkId",
                table: "Quizzes",
                column: "LinkId",
                principalTable: "Links",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Links_LinkId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_LinkId",
                table: "Quizzes");

            migrationBuilder.AlterColumn<int>(
                name: "LinkId",
                table: "Quizzes",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Quizzes",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<Guid>(
                name: "LinkId1",
                table: "Quizzes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "PasswordHash",
                value: "$2a$12$P.NKNVluVPYyQq8NHVUQH..Y0uN.9Y5SxUTdF2WQpYVNK9e0fEdQG");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_LinkId1",
                table: "Quizzes",
                column: "LinkId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Links_LinkId1",
                table: "Quizzes",
                column: "LinkId1",
                principalTable: "Links",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
