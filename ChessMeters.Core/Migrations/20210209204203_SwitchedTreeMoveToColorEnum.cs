using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessMeters.Core.Migrations
{
    public partial class SwitchedTreeMoveToColorEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "ColorId",
                table: "TreeMoves",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.Sql("UPDATE TreeMoves SET ColorId = (IF(ISNULL(FullPath), 0, MOD(LENGTH(FullPath) - LENGTH(REPLACE(FullPath, ' ', '')), 2) = 0)) + 1");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "TreeMoves");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "TreeMoves");

            migrationBuilder.AddColumn<bool>(
                name: "Color",
                table: "TreeMoves",
                type: "tinyint(1)",
                nullable: false,
                computedColumnSql: "IF(ISNULL(FullPath), 0, MOD(LENGTH(FullPath) - LENGTH(REPLACE(FullPath, ' ', '')), 2) = 0)");
        }
    }
}
