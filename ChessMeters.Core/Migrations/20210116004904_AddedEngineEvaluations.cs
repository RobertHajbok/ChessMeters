using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessMeters.Core.Migrations
{
    public partial class AddedEngineEvaluations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<short>(
                name: "Id",
                table: "Engines",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "EngineEvaluations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TreeMoveId = table.Column<long>(type: "bigint", nullable: true),
                    EngineId = table.Column<short>(type: "smallint", nullable: false),
                    Depth = table.Column<short>(type: "smallint", nullable: false),
                    EvaluationCentipawns = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineEvaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EngineEvaluations_Engines_EngineId",
                        column: x => x.EngineId,
                        principalTable: "Engines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EngineEvaluations_TreeMoves_TreeMoveId",
                        column: x => x.TreeMoveId,
                        principalTable: "TreeMoves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Engines",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { (short)1, "Stockfish v12", "Stockfish12" });

            migrationBuilder.CreateIndex(
                name: "IX_EngineEvaluations_EngineId",
                table: "EngineEvaluations",
                column: "EngineId");

            migrationBuilder.CreateIndex(
                name: "IX_EngineEvaluations_TreeMoveId",
                table: "EngineEvaluations",
                column: "TreeMoveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EngineEvaluations");

            migrationBuilder.DeleteData(
                table: "Engines",
                keyColumn: "Id",
                keyValue: (short)1);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Engines",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.InsertData(
                table: "Engines",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Stockfish v12", "Stockfish12" });
        }
    }
}
