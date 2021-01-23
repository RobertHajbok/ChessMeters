using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessMeters.Core.Migrations
{
    public partial class AddedMoreGameProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "BlackElo",
                table: "Games",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "Eco",
                table: "Games",
                type: "varchar(3) CHARACTER SET utf8mb4",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Termination",
                table: "Games",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeControl",
                table: "Games",
                type: "varchar(100) CHARACTER SET utf8mb4",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "WhiteElo",
                table: "Games",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlackElo",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Eco",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Termination",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "TimeControl",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "WhiteElo",
                table: "Games");
        }
    }
}
