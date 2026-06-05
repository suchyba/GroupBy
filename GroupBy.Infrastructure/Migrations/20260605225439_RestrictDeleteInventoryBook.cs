using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupBy.Data.Migrations
{
    /// <inheritdoc />
    public partial class RestrictDeleteInventoryBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryBookRecords_InventoryBooks_InventoryBookId",
                table: "InventoryBookRecords");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryBookRecords_InventoryBooks_InventoryBookId",
                table: "InventoryBookRecords",
                column: "InventoryBookId",
                principalTable: "InventoryBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryBookRecords_InventoryBooks_InventoryBookId",
                table: "InventoryBookRecords");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryBookRecords_InventoryBooks_InventoryBookId",
                table: "InventoryBookRecords",
                column: "InventoryBookId",
                principalTable: "InventoryBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
