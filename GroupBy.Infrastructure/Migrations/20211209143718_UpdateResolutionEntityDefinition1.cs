using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupBy.Data.Migrations
{
    public partial class UpdateResolutionEntityDefinition1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PositionRecords_Resolutions_AppointingResolutionGroupId_AppointingResolutionId",
                table: "PositionRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_PositionRecords_Resolutions_DismissingResolutionGroupId_DismissingResolutionId",
                table: "PositionRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resolutions",
                table: "Resolutions");

            migrationBuilder.DropIndex(
                name: "IX_PositionRecords_AppointingResolutionGroupId_AppointingResolutionId",
                table: "PositionRecords");

            migrationBuilder.DropIndex(
                name: "IX_PositionRecords_DismissingResolutionGroupId_DismissingResolutionId",
                table: "PositionRecords");

            migrationBuilder.DropColumn(
                name: "AppointingResolutionGroupId",
                table: "PositionRecords");

            migrationBuilder.DropColumn(
                name: "AppointingResolutionId",
                table: "PositionRecords");

            migrationBuilder.RenameColumn(
                name: "DismissingResolutionId",
                table: "PositionRecords",
                newName: "DismissingResolutionIdRes");

            migrationBuilder.RenameColumn(
                name: "DismissingResolutionGroupId",
                table: "PositionRecords",
                newName: "AppointingResolutionIdRes");

            migrationBuilder.AddColumn<int>(
                name: "IdRes",
                table: "Resolutions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resolutions",
                table: "Resolutions",
                column: "IdRes");

            migrationBuilder.CreateIndex(
                name: "IX_Resolutions_GroupId",
                table: "Resolutions",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionRecords_AppointingResolutionIdRes",
                table: "PositionRecords",
                column: "AppointingResolutionIdRes");

            migrationBuilder.CreateIndex(
                name: "IX_PositionRecords_DismissingResolutionIdRes",
                table: "PositionRecords",
                column: "DismissingResolutionIdRes");

            migrationBuilder.AddForeignKey(
                name: "FK_PositionRecords_Resolutions_AppointingResolutionIdRes",
                table: "PositionRecords",
                column: "AppointingResolutionIdRes",
                principalTable: "Resolutions",
                principalColumn: "IdRes",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PositionRecords_Resolutions_DismissingResolutionIdRes",
                table: "PositionRecords",
                column: "DismissingResolutionIdRes",
                principalTable: "Resolutions",
                principalColumn: "IdRes",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PositionRecords_Resolutions_AppointingResolutionIdRes",
                table: "PositionRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_PositionRecords_Resolutions_DismissingResolutionIdRes",
                table: "PositionRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resolutions",
                table: "Resolutions");

            migrationBuilder.DropIndex(
                name: "IX_Resolutions_GroupId",
                table: "Resolutions");

            migrationBuilder.DropIndex(
                name: "IX_PositionRecords_AppointingResolutionIdRes",
                table: "PositionRecords");

            migrationBuilder.DropIndex(
                name: "IX_PositionRecords_DismissingResolutionIdRes",
                table: "PositionRecords");

            migrationBuilder.DropColumn(
                name: "IdRes",
                table: "Resolutions");

            migrationBuilder.RenameColumn(
                name: "DismissingResolutionIdRes",
                table: "PositionRecords",
                newName: "DismissingResolutionId");

            migrationBuilder.RenameColumn(
                name: "AppointingResolutionIdRes",
                table: "PositionRecords",
                newName: "DismissingResolutionGroupId");

            migrationBuilder.AddColumn<int>(
                name: "AppointingResolutionGroupId",
                table: "PositionRecords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppointingResolutionId",
                table: "PositionRecords",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resolutions",
                table: "Resolutions",
                columns: new[] { "GroupId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_PositionRecords_AppointingResolutionGroupId_AppointingResolutionId",
                table: "PositionRecords",
                columns: new[] { "AppointingResolutionGroupId", "AppointingResolutionId" });

            migrationBuilder.CreateIndex(
                name: "IX_PositionRecords_DismissingResolutionGroupId_DismissingResolutionId",
                table: "PositionRecords",
                columns: new[] { "DismissingResolutionGroupId", "DismissingResolutionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PositionRecords_Resolutions_AppointingResolutionGroupId_AppointingResolutionId",
                table: "PositionRecords",
                columns: new[] { "AppointingResolutionGroupId", "AppointingResolutionId" },
                principalTable: "Resolutions",
                principalColumns: new[] { "GroupId", "Id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PositionRecords_Resolutions_DismissingResolutionGroupId_DismissingResolutionId",
                table: "PositionRecords",
                columns: new[] { "DismissingResolutionGroupId", "DismissingResolutionId" },
                principalTable: "Resolutions",
                principalColumns: new[] { "GroupId", "Id" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
