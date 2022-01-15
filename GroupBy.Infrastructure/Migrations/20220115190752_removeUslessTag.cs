using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupBy.Data.Migrations
{
    public partial class removeUslessTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryBookRecords_InventoryItemSources_SourceId",
                table: "InventoryBookRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_PositionRecords_Groups_RelatedGroupId",
                table: "PositionRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_PositionRecords_Positions_PositionId",
                table: "PositionRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistrationCodes_Groups_TargetGroupId",
                table: "RegistrationCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Resolutions_Volunteers_LegislatorId",
                table: "Resolutions");

            migrationBuilder.AlterColumn<int>(
                name: "LegislatorId",
                table: "Resolutions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TargetGroupId",
                table: "RegistrationCodes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RelatedGroupId",
                table: "PositionRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "PositionRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SourceId",
                table: "InventoryBookRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryBookRecords_InventoryItemSources_SourceId",
                table: "InventoryBookRecords",
                column: "SourceId",
                principalTable: "InventoryItemSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PositionRecords_Groups_RelatedGroupId",
                table: "PositionRecords",
                column: "RelatedGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PositionRecords_Positions_PositionId",
                table: "PositionRecords",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrationCodes_Groups_TargetGroupId",
                table: "RegistrationCodes",
                column: "TargetGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resolutions_Volunteers_LegislatorId",
                table: "Resolutions",
                column: "LegislatorId",
                principalTable: "Volunteers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryBookRecords_InventoryItemSources_SourceId",
                table: "InventoryBookRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_PositionRecords_Groups_RelatedGroupId",
                table: "PositionRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_PositionRecords_Positions_PositionId",
                table: "PositionRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistrationCodes_Groups_TargetGroupId",
                table: "RegistrationCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Resolutions_Volunteers_LegislatorId",
                table: "Resolutions");

            migrationBuilder.AlterColumn<int>(
                name: "LegislatorId",
                table: "Resolutions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TargetGroupId",
                table: "RegistrationCodes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RelatedGroupId",
                table: "PositionRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "PositionRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SourceId",
                table: "InventoryBookRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryBookRecords_InventoryItemSources_SourceId",
                table: "InventoryBookRecords",
                column: "SourceId",
                principalTable: "InventoryItemSources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PositionRecords_Groups_RelatedGroupId",
                table: "PositionRecords",
                column: "RelatedGroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PositionRecords_Positions_PositionId",
                table: "PositionRecords",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrationCodes_Groups_TargetGroupId",
                table: "RegistrationCodes",
                column: "TargetGroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resolutions_Volunteers_LegislatorId",
                table: "Resolutions",
                column: "LegislatorId",
                principalTable: "Volunteers",
                principalColumn: "Id");
        }
    }
}
