using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupBy.Data.Migrations
{
    public partial class CorrectFieldInventoryItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descryption",
                table: "InventoryItems",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "InventoryItems",
                newName: "Descryption");
        }
    }
}
