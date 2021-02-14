using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessMeters.Core.Migrations
{
    public partial class AddedLinkedAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChessComUsername",
                table: "AspNetUsers",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LichessUsername",
                table: "AspNetUsers",
                type: "varchar(20) CHARACTER SET utf8mb4",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChessComUsername",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LichessUsername",
                table: "AspNetUsers");
        }
    }
}
