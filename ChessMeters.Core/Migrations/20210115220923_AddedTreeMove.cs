using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessMeters.Core.Migrations
{
    public partial class AddedTreeMove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TreeMoves",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Move = table.Column<string>(type: "varchar(5) CHARACTER SET utf8mb4", maxLength: 5, nullable: true),
                    Color = table.Column<bool>(type: "tinyint(1)", nullable: false, computedColumnSql: "(LENGTH(FullPath) - LENGTH(REPLACE(FullPath, ' ', ''))) / 2 <> 0"),
                    ParentTreeMoveId = table.Column<long>(type: "bigint", nullable: true),
                    FullPath = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreeMoves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreeMoves_TreeMoves_ParentTreeMoveId",
                        column: x => x.ParentTreeMoveId,
                        principalTable: "TreeMoves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TreeMoves_ParentTreeMoveId",
                table: "TreeMoves",
                column: "ParentTreeMoveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreeMoves");
        }
    }
}
