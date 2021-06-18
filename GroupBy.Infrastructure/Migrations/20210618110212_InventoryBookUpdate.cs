using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupBy.Data.Migrations
{
    public partial class InventoryBookUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "InventoryBooks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "InventoryBooks");
        }
    }
}
