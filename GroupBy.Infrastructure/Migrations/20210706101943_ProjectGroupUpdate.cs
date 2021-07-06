using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupBy.Data.Migrations
{
    public partial class ProjectGroupUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Projects_ProjectId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_ProjectId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Groups");

            migrationBuilder.AddColumn<int>(
                name: "ProjectGroupId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectGroupId",
                table: "Projects",
                column: "ProjectGroupId",
                unique: true,
                filter: "[ProjectGroupId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Groups_ProjectGroupId",
                table: "Projects",
                column: "ProjectGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Groups_ProjectGroupId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectGroupId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectGroupId",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Groups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ProjectId",
                table: "Groups",
                column: "ProjectId",
                unique: true,
                filter: "[ProjectId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Projects_ProjectId",
                table: "Groups",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
