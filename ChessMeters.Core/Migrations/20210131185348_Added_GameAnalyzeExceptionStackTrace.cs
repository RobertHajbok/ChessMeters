using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessMeters.Core.Migrations
{
    public partial class Added_GameAnalyzeExceptionStackTrace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnalyzeExceptionStackTrace",
                table: "Games",
                type: "varchar(2000) CHARACTER SET utf8mb4",
                maxLength: 2000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnalyzeExceptionStackTrace",
                table: "Games");
        }
    }
}
