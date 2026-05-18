using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using TeachingBACKEND.Data;

#nullable disable

#pragma warning disable CA1814

namespace TeachingBACKEND.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20260515000001_AddDragDropPayloads")]
    public partial class AddDragDropPayloads : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DragSpellPayloads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizzId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Letters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DragSpellPayloads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DragSpellPayloads_Files_ImageFileId",
                        column: x => x.ImageFileId,
                        principalTable: "Files",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DragSpellPayloads_Quizzes_QuizzId",
                        column: x => x.QuizzId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DragOrderPayloads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizzId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorrectOrder = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DragOrderPayloads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DragOrderPayloads_Quizzes_QuizzId",
                        column: x => x.QuizzId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DragOrderTiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DragOrderPayloadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DragOrderTiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DragOrderTiles_DragOrderPayloads_DragOrderPayloadId",
                        column: x => x.DragOrderPayloadId,
                        principalTable: "DragOrderPayloads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DragMatchPayloads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizzId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DragMatchPayloads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DragMatchPayloads_Quizzes_QuizzId",
                        column: x => x.QuizzId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DragMatchPairs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DragMatchPayloadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DragMatchPairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DragMatchPairs_DragMatchPayloads_DragMatchPayloadId",
                        column: x => x.DragMatchPayloadId,
                        principalTable: "DragMatchPayloads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DragMatchPairs_Files_ImageFileId",
                        column: x => x.ImageFileId,
                        principalTable: "Files",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DragSpellPayloads_QuizzId",
                table: "DragSpellPayloads",
                column: "QuizzId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DragSpellPayloads_ImageFileId",
                table: "DragSpellPayloads",
                column: "ImageFileId");

            migrationBuilder.CreateIndex(
                name: "IX_DragOrderPayloads_QuizzId",
                table: "DragOrderPayloads",
                column: "QuizzId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DragOrderTiles_DragOrderPayloadId",
                table: "DragOrderTiles",
                column: "DragOrderPayloadId");

            migrationBuilder.CreateIndex(
                name: "IX_DragMatchPayloads_QuizzId",
                table: "DragMatchPayloads",
                column: "QuizzId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DragMatchPairs_DragMatchPayloadId",
                table: "DragMatchPairs",
                column: "DragMatchPayloadId");

            migrationBuilder.CreateIndex(
                name: "IX_DragMatchPairs_ImageFileId",
                table: "DragMatchPairs",
                column: "ImageFileId");

            migrationBuilder.Sql(@"
                INSERT INTO [QuizTypes] ([Id], [Name]) VALUES
                (N'c1c1c1c1-c1c1-1111-1111-111111111111', N'DragSpell'),
                (N'c2c2c2c2-c2c2-2222-2222-222222222222', N'DragOrder'),
                (N'c3c3c3c3-c3c3-3333-3333-333333333333', N'DragMatch')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "DragMatchPairs");
            migrationBuilder.DropTable(name: "DragMatchPayloads");
            migrationBuilder.DropTable(name: "DragOrderTiles");
            migrationBuilder.DropTable(name: "DragOrderPayloads");
            migrationBuilder.DropTable(name: "DragSpellPayloads");

            migrationBuilder.Sql(@"
                DELETE FROM [QuizTypes] WHERE [Id] IN (
                    N'c1c1c1c1-c1c1-1111-1111-111111111111',
                    N'c2c2c2c2-c2c2-2222-2222-222222222222',
                    N'c3c3c3c3-c3c3-3333-3333-333333333333'
                )");
        }
    }
}
