using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessMeters.Core.Migrations
{
    public partial class AddedGameProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Black",
                table: "Games",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Event",
                table: "Games",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Round",
                table: "Games",
                type: "varchar(50) CHARACTER SET utf8mb4",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Site",
                table: "Games",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "White",
                table: "Games",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Black",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Event",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Round",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Site",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "White",
                table: "Games");
        }
    }
}
