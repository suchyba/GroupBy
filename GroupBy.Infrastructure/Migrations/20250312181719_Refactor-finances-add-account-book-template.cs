using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupBy.Data.Migrations
{
    /// <inheritdoc />
    public partial class Refactorfinancesaddaccountbooktemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountingBookFinancialCategory_FinancialCategory_CategoriesId",
                table: "AccountingBookFinancialCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialCategoryValue_FinancialCategory_CategoryId",
                table: "FinancialCategoryValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialCategory",
                table: "FinancialCategory");

            migrationBuilder.RenameTable(
                name: "FinancialCategory",
                newName: "FinancialCategories");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountingBookTemplateId",
                table: "FinancialCategories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialCategories",
                table: "FinancialCategories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AccountingBookTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingBookTemplates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialCategories_AccountingBookTemplateId",
                table: "FinancialCategories",
                column: "AccountingBookTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingBookFinancialCategory_FinancialCategories_CategoriesId",
                table: "AccountingBookFinancialCategory",
                column: "CategoriesId",
                principalTable: "FinancialCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialCategories_AccountingBookTemplates_AccountingBookTemplateId",
                table: "FinancialCategories",
                column: "AccountingBookTemplateId",
                principalTable: "AccountingBookTemplates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialCategoryValue_FinancialCategories_CategoryId",
                table: "FinancialCategoryValue",
                column: "CategoryId",
                principalTable: "FinancialCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountingBookFinancialCategory_FinancialCategories_CategoriesId",
                table: "AccountingBookFinancialCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialCategories_AccountingBookTemplates_AccountingBookTemplateId",
                table: "FinancialCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialCategoryValue_FinancialCategories_CategoryId",
                table: "FinancialCategoryValue");

            migrationBuilder.DropTable(
                name: "AccountingBookTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialCategories",
                table: "FinancialCategories");

            migrationBuilder.DropIndex(
                name: "IX_FinancialCategories_AccountingBookTemplateId",
                table: "FinancialCategories");

            migrationBuilder.DropColumn(
                name: "AccountingBookTemplateId",
                table: "FinancialCategories");

            migrationBuilder.RenameTable(
                name: "FinancialCategories",
                newName: "FinancialCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialCategory",
                table: "FinancialCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingBookFinancialCategory_FinancialCategory_CategoriesId",
                table: "AccountingBookFinancialCategory",
                column: "CategoriesId",
                principalTable: "FinancialCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialCategoryValue_FinancialCategory_CategoryId",
                table: "FinancialCategoryValue",
                column: "CategoryId",
                principalTable: "FinancialCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
