using Microsoft.EntityFrameworkCore.Migrations;

namespace Borg.Platform.Backoffice.Security.EF.data.Migrations
{
    public partial class fifth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SurName",
                schema: "security",
                table: "CmsUser",
                newName: "Surname");

            migrationBuilder.AddColumn<bool>(
                name: "IsSystem",
                schema: "security",
                table: "CmsRole",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSystem",
                schema: "security",
                table: "CmsRole");

            migrationBuilder.RenameColumn(
                name: "Surname",
                schema: "security",
                table: "CmsUser",
                newName: "SurName");
        }
    }
}
