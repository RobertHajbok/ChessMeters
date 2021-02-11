using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessMeters.Core.Migrations
{
    public partial class ChangeGameUserColorNotNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Colors_UserColorId",
                table: "Games");

            migrationBuilder.Sql("DELETE FROM Games WHERE UserColorId IS NULL");

            migrationBuilder.AlterColumn<short>(
                name: "UserColorId",
                table: "Games",
                type: "smallint",
                nullable: false,
                defaultValue: (short)1,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Colors_UserColorId",
                table: "Games",
                column: "UserColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Colors_UserColorId",
                table: "Games");

            migrationBuilder.AlterColumn<short>(
                name: "UserColorId",
                table: "Games",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Colors_UserColorId",
                table: "Games",
                column: "UserColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
