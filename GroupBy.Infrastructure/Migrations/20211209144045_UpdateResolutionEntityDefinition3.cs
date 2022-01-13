using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupBy.Data.Migrations
{
    public partial class UpdateResolutionEntityDefinition3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PositionRecords_Resolutions_AppointingResolutionIdRes",
                table: "PositionRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_PositionRecords_Resolutions_DismissingResolutionIdRes",
                table: "PositionRecords");

            migrationBuilder.RenameColumn(
                name: "IdRes",
                table: "Resolutions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DismissingResolutionIdRes",
                table: "PositionRecords",
                newName: "DismissingResolutionId");

            migrationBuilder.RenameColumn(
                name: "AppointingResolutionIdRes",
                table: "PositionRecords",
                newName: "AppointingResolutionId");

            migrationBuilder.RenameIndex(
                name: "IX_PositionRecords_DismissingResolutionIdRes",
                table: "PositionRecords",
                newName: "IX_PositionRecords_DismissingResolutionId");

            migrationBuilder.RenameIndex(
                name: "IX_PositionRecords_AppointingResolutionIdRes",
                table: "PositionRecords",
                newName: "IX_PositionRecords_AppointingResolutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PositionRecords_Resolutions_AppointingResolutionId",
                table: "PositionRecords",
                column: "AppointingResolutionId",
                principalTable: "Resolutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PositionRecords_Resolutions_DismissingResolutionId",
                table: "PositionRecords",
                column: "DismissingResolutionId",
                principalTable: "Resolutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PositionRecords_Resolutions_AppointingResolutionId",
                table: "PositionRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_PositionRecords_Resolutions_DismissingResolutionId",
                table: "PositionRecords");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Resolutions",
                newName: "IdRes");

            migrationBuilder.RenameColumn(
                name: "DismissingResolutionId",
                table: "PositionRecords",
                newName: "DismissingResolutionIdRes");

            migrationBuilder.RenameColumn(
                name: "AppointingResolutionId",
                table: "PositionRecords",
                newName: "AppointingResolutionIdRes");

            migrationBuilder.RenameIndex(
                name: "IX_PositionRecords_DismissingResolutionId",
                table: "PositionRecords",
                newName: "IX_PositionRecords_DismissingResolutionIdRes");

            migrationBuilder.RenameIndex(
                name: "IX_PositionRecords_AppointingResolutionId",
                table: "PositionRecords",
                newName: "IX_PositionRecords_AppointingResolutionIdRes");

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
    }
}
