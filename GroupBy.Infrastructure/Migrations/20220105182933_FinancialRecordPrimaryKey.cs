using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupBy.Data.Migrations
{
    public partial class FinancialRecordPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialRecords",
                table: "FinancialRecords");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FinancialRecords");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FinancialRecords",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialRecords",
                table: "FinancialRecords",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialRecords_BookId_BookOrderNumberId",
                table: "FinancialRecords",
                columns: new[] { "BookId", "BookOrderNumberId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialRecords",
                table: "FinancialRecords");

            migrationBuilder.DropIndex(
                name: "IX_FinancialRecords_BookId_BookOrderNumberId",
                table: "FinancialRecords");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FinancialRecords",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialRecords",
                table: "FinancialRecords",
                columns: new[] { "BookId", "BookOrderNumberId", "Id" });
        }
    }
}
