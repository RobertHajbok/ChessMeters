using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessMeters.Core.Migrations
{
    public partial class LinkedGameToReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReportId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Games_ReportId",
                table: "Games",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Reports_ReportId",
                table: "Games",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Reports_ReportId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_ReportId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "Games");
        }
    }
}
