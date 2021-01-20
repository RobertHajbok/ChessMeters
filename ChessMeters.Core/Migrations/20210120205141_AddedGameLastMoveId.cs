using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessMeters.Core.Migrations
{
    public partial class AddedGameLastMoveId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LastTreeMoveId",
                table: "Games",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_LastTreeMoveId",
                table: "Games",
                column: "LastTreeMoveId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_TreeMoves_LastTreeMoveId",
                table: "Games",
                column: "LastTreeMoveId",
                principalTable: "TreeMoves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_TreeMoves_LastTreeMoveId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_LastTreeMoveId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "LastTreeMoveId",
                table: "Games");
        }
    }
}
