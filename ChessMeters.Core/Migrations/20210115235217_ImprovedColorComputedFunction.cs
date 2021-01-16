using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessMeters.Core.Migrations
{
    public partial class ImprovedColorComputedFunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Color",
                table: "TreeMoves",
                type: "tinyint(1)",
                nullable: false,
                computedColumnSql: "IF(ISNULL(FullPath), 0, (LENGTH(FullPath) - LENGTH(REPLACE(FullPath, ' ', '')))) / 2 <> 0",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComputedColumnSql: "(LENGTH(FullPath) - LENGTH(REPLACE(FullPath, ' ', ''))) / 2 <> 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Color",
                table: "TreeMoves",
                type: "tinyint(1)",
                nullable: false,
                computedColumnSql: "(LENGTH(FullPath) - LENGTH(REPLACE(FullPath, ' ', ''))) / 2 <> 0",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComputedColumnSql: "IF(ISNULL(FullPath), 0, (LENGTH(FullPath) - LENGTH(REPLACE(FullPath, ' ', '')))) / 2 <> 0");
        }
    }
}
