using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessMeters.Core.Migrations
{
    public partial class AddedInaccuracyAndMistakeFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Flags",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 5, "This flag should be raised when the current move is an inaccuracy in the given positon", "Inaccuracy" });

            migrationBuilder.InsertData(
                table: "Flags",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 6, "This flag should be raised when the current move is a mistake in the given positon", "Mistake" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Flags",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Flags",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
