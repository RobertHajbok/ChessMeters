using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessMeters.Core.Migrations
{
    public partial class EngineEvaluationTreeMoveIdNotNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngineEvaluations_TreeMoves_TreeMoveId",
                table: "EngineEvaluations");

            migrationBuilder.AlterColumn<long>(
                name: "TreeMoveId",
                table: "EngineEvaluations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EngineEvaluations_TreeMoves_TreeMoveId",
                table: "EngineEvaluations",
                column: "TreeMoveId",
                principalTable: "TreeMoves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngineEvaluations_TreeMoves_TreeMoveId",
                table: "EngineEvaluations");

            migrationBuilder.AlterColumn<long>(
                name: "TreeMoveId",
                table: "EngineEvaluations",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_EngineEvaluations_TreeMoves_TreeMoveId",
                table: "EngineEvaluations",
                column: "TreeMoveId",
                principalTable: "TreeMoves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
