using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupBy.Data.Migrations
{
    /// <inheritdoc />
    public partial class Refactorfinancesfixrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialCategory_AccountingBooks_AccountingBookId",
                table: "FinancialCategory");

            migrationBuilder.DropIndex(
                name: "IX_FinancialCategory_AccountingBookId",
                table: "FinancialCategory");

            migrationBuilder.DropColumn(
                name: "AccountingBookId",
                table: "FinancialCategory");

            migrationBuilder.CreateTable(
                name: "AccountingBookFinancialCategory",
                columns: table => new
                {
                    AccountingBooksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingBookFinancialCategory", x => new { x.AccountingBooksId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_AccountingBookFinancialCategory_AccountingBooks_AccountingBooksId",
                        column: x => x.AccountingBooksId,
                        principalTable: "AccountingBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountingBookFinancialCategory_FinancialCategory_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "FinancialCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountingBookFinancialCategory_CategoriesId",
                table: "AccountingBookFinancialCategory",
                column: "CategoriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountingBookFinancialCategory");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountingBookId",
                table: "FinancialCategory",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialCategory_AccountingBookId",
                table: "FinancialCategory",
                column: "AccountingBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialCategory_AccountingBooks_AccountingBookId",
                table: "FinancialCategory",
                column: "AccountingBookId",
                principalTable: "AccountingBooks",
                principalColumn: "Id");
        }
    }
}
