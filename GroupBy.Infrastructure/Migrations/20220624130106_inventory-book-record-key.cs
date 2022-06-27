using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupBy.Data.Migrations
{
    public partial class inventorybookrecordkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryBookRecords",
                table: "InventoryBookRecords");

            migrationBuilder.DropColumn(name: "Id", table: "InventoryBookRecords");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "InventoryBookRecords",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryBookRecords",
                table: "InventoryBookRecords",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBookRecords_InventoryBookId",
                table: "InventoryBookRecords",
                column: "InventoryBookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryBookRecords",
                table: "InventoryBookRecords");

            migrationBuilder.DropIndex(
                name: "IX_InventoryBookRecords_InventoryBookId",
                table: "InventoryBookRecords");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "InventoryBookRecords",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryBookRecords",
                table: "InventoryBookRecords",
                columns: new[] { "InventoryBookId", "Id" });
        }
    }
}
