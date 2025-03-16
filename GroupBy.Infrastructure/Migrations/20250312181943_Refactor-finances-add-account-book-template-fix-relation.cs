using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupBy.Data.Migrations
{
    /// <inheritdoc />
    public partial class Refactorfinancesaddaccountbooktemplatefixrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialCategories_AccountingBookTemplates_AccountingBookTemplateId",
                table: "FinancialCategories");

            migrationBuilder.DropIndex(
                name: "IX_FinancialCategories_AccountingBookTemplateId",
                table: "FinancialCategories");

            migrationBuilder.DropColumn(
                name: "AccountingBookTemplateId",
                table: "FinancialCategories");

            migrationBuilder.CreateTable(
                name: "AccountingBookTemplateFinancialCategory",
                columns: table => new
                {
                    AccountingBookTemplatesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingBookTemplateFinancialCategory", x => new { x.AccountingBookTemplatesId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_AccountingBookTemplateFinancialCategory_AccountingBookTemplates_AccountingBookTemplatesId",
                        column: x => x.AccountingBookTemplatesId,
                        principalTable: "AccountingBookTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountingBookTemplateFinancialCategory_FinancialCategories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "FinancialCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountingBookTemplateFinancialCategory_CategoriesId",
                table: "AccountingBookTemplateFinancialCategory",
                column: "CategoriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountingBookTemplateFinancialCategory");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountingBookTemplateId",
                table: "FinancialCategories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialCategories_AccountingBookTemplateId",
                table: "FinancialCategories",
                column: "AccountingBookTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialCategories_AccountingBookTemplates_AccountingBookTemplateId",
                table: "FinancialCategories",
                column: "AccountingBookTemplateId",
                principalTable: "AccountingBookTemplates",
                principalColumn: "Id");
        }
    }
}
