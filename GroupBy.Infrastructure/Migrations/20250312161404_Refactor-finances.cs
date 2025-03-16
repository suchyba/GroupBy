using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupBy.Data.Migrations
{
    /// <inheritdoc />
    public partial class Refactorfinances : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accommodation",
                table: "FinancialRecords");

            migrationBuilder.DropColumn(
                name: "Dotation",
                table: "FinancialRecords");

            migrationBuilder.DropColumn(
                name: "EarningAction",
                table: "FinancialRecords");

            migrationBuilder.DropColumn(
                name: "FinancialIncomeRecord_Other",
                table: "FinancialRecords");

            migrationBuilder.DropColumn(
                name: "Food",
                table: "FinancialRecords");

            migrationBuilder.DropColumn(
                name: "Insurance",
                table: "FinancialRecords");

            migrationBuilder.DropColumn(
                name: "Inventory",
                table: "FinancialRecords");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "FinancialRecords");

            migrationBuilder.DropColumn(
                name: "MembershipFee",
                table: "FinancialRecords");

            migrationBuilder.DropColumn(
                name: "OnePercent",
                table: "FinancialRecords");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "FinancialRecords");

            migrationBuilder.DropColumn(
                name: "ProgramFee",
                table: "FinancialRecords");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "FinancialRecords");

            migrationBuilder.DropColumn(
                name: "Service",
                table: "FinancialRecords");

            migrationBuilder.DropColumn(
                name: "Transport",
                table: "FinancialRecords");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "FinancialRecords",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Elements",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "FinancialCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Income = table.Column<bool>(type: "bit", nullable: false),
                    AccountingBookId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialCategory_AccountingBooks_AccountingBookId",
                        column: x => x.AccountingBookId,
                        principalTable: "AccountingBooks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FinancialCategoryValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FinancialRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialCategoryValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialCategoryValue_FinancialCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "FinancialCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinancialCategoryValue_FinancialRecords_FinancialRecordId",
                        column: x => x.FinancialRecordId,
                        principalTable: "FinancialRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialCategory_AccountingBookId",
                table: "FinancialCategory",
                column: "AccountingBookId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialCategoryValue_CategoryId",
                table: "FinancialCategoryValue",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialCategoryValue_FinancialRecordId",
                table: "FinancialCategoryValue",
                column: "FinancialRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialCategoryValue");

            migrationBuilder.DropTable(
                name: "FinancialCategory");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "FinancialRecords",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(34)",
                oldMaxLength: 34);

            migrationBuilder.AddColumn<decimal>(
                name: "Accommodation",
                table: "FinancialRecords",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Dotation",
                table: "FinancialRecords",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EarningAction",
                table: "FinancialRecords",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FinancialIncomeRecord_Other",
                table: "FinancialRecords",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Food",
                table: "FinancialRecords",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Insurance",
                table: "FinancialRecords",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Inventory",
                table: "FinancialRecords",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Material",
                table: "FinancialRecords",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MembershipFee",
                table: "FinancialRecords",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OnePercent",
                table: "FinancialRecords",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Other",
                table: "FinancialRecords",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ProgramFee",
                table: "FinancialRecords",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "FinancialRecords",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Service",
                table: "FinancialRecords",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Transport",
                table: "FinancialRecords",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Elements",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(21)",
                oldMaxLength: 21);
        }
    }
}
