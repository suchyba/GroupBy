using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupBy.Data.Migrations
{
    public partial class ShareElementToGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Elements_Groups_GroupId",
                table: "Elements");

            migrationBuilder.DropIndex(
                name: "IX_Elements_GroupId",
                table: "Elements");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Elements");

            migrationBuilder.CreateTable(
                name: "ElementGroup",
                columns: table => new
                {
                    ElementsId = table.Column<int>(type: "int", nullable: false),
                    GroupsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementGroup", x => new { x.ElementsId, x.GroupsId });
                    table.ForeignKey(
                        name: "FK_ElementGroup_Elements_ElementsId",
                        column: x => x.ElementsId,
                        principalTable: "Elements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElementGroup_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElementGroup_GroupsId",
                table: "ElementGroup",
                column: "GroupsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElementGroup");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Elements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Elements_GroupId",
                table: "Elements",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Elements_Groups_GroupId",
                table: "Elements",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
