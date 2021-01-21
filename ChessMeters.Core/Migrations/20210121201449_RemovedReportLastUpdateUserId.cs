using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessMeters.Core.Migrations
{
    public partial class RemovedReportLastUpdateUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AspNetUsers_LastUpdateUserId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_LastUpdateUserId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "LastUpdateUserId",
                table: "Reports");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastUpdateUserId",
                table: "Reports",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_LastUpdateUserId",
                table: "Reports",
                column: "LastUpdateUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AspNetUsers_LastUpdateUserId",
                table: "Reports",
                column: "LastUpdateUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
