using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupBy.Data.Migrations
{
    public partial class inventorybookrecorddocumentupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Document",
                table: "InventoryBookRecords");

            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "InventoryBookRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBookRecords_DocumentId",
                table: "InventoryBookRecords",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryBookRecords_Elements_DocumentId",
                table: "InventoryBookRecords",
                column: "DocumentId",
                principalTable: "Elements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryBookRecords_Elements_DocumentId",
                table: "InventoryBookRecords");

            migrationBuilder.DropIndex(
                name: "IX_InventoryBookRecords_DocumentId",
                table: "InventoryBookRecords");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "InventoryBookRecords");

            migrationBuilder.AddColumn<string>(
                name: "Document",
                table: "InventoryBookRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
