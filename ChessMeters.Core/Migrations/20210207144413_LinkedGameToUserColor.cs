using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessMeters.Core.Migrations
{
    public partial class LinkedGameToUserColor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "UserColorId",
                table: "Games",
                type: "smallint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_UserColorId",
                table: "Games",
                column: "UserColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Colors_UserColorId",
                table: "Games",
                column: "UserColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Colors_UserColorId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_UserColorId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "UserColorId",
                table: "Games");
        }
    }
}
