using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupBy.Data.Migrations
{
    public partial class AdjustRegistrationCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RegistrationCodes",
                table: "RegistrationCodes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RegistrationCodes");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "RegistrationCodes");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "RegistrationCodes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegistrationCodes",
                table: "RegistrationCodes",
                column: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RegistrationCodes",
                table: "RegistrationCodes");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "RegistrationCodes");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RegistrationCodes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "RegistrationCodes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegistrationCodes",
                table: "RegistrationCodes",
                column: "Id");
        }
    }
}
