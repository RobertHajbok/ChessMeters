using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessMeters.Core.Migrations
{
    public partial class AddedGameAndTreeMoveFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50) CHARACTER SET utf8mb4", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(100) CHARACTER SET utf8mb4", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameFlags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    FlagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameFlags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameFlags_Flags_FlagId",
                        column: x => x.FlagId,
                        principalTable: "Flags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameFlags_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TreeMoveFlags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TreeMoveId = table.Column<long>(type: "bigint", nullable: false),
                    FlagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreeMoveFlags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreeMoveFlags_Flags_FlagId",
                        column: x => x.FlagId,
                        principalTable: "Flags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TreeMoveFlags_TreeMoves_TreeMoveId",
                        column: x => x.TreeMoveId,
                        principalTable: "TreeMoves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Flags",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "This flag should be raised when the current move is a blunder in the given positon", "Blunder" },
                    { 2, "This flag should be raised when player hasn't castled before a given move", "DidNotCastle" },
                    { 3, "This flag should be raised when player didn't develop all his or her pieces in the opening", "DidNotDevelopAllPieces" },
                    { 4, "This flag should be raised when no minor piece was developed in the first 5 moves", "DidNotDevelopAtLeastOneMinorPieceBeforeMove5" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameFlags_FlagId",
                table: "GameFlags",
                column: "FlagId");

            migrationBuilder.CreateIndex(
                name: "IX_GameFlags_GameId",
                table: "GameFlags",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_TreeMoveFlags_FlagId",
                table: "TreeMoveFlags",
                column: "FlagId");

            migrationBuilder.CreateIndex(
                name: "IX_TreeMoveFlags_TreeMoveId",
                table: "TreeMoveFlags",
                column: "TreeMoveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameFlags");

            migrationBuilder.DropTable(
                name: "TreeMoveFlags");

            migrationBuilder.DropTable(
                name: "Flags");
        }
    }
}
