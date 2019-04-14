using Microsoft.EntityFrameworkCore.Migrations;

namespace Borg.Platform.Backoffice.Security.EF.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.CreateSequence<int>(
                name: "CmsRole_Id");

            migrationBuilder.CreateSequence<int>(
                name: "CmsRolePermission_Id");

            migrationBuilder.CreateSequence<int>(
                name: "CmsUser_Id");

            migrationBuilder.CreateSequence<int>(
                name: "CmsUserPermission_Id");

            migrationBuilder.CreateTable(
                name: "CmsRole",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR CmsRole_Id"),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsRole", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "CmsRolePermission",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR CmsRolePermission_Id"),
                    ParentId = table.Column<int>(nullable: false),
                    Depth = table.Column<int>(nullable: false),
                    Resource = table.Column<string>(nullable: true),
                    PermissionOperation = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsRolePermission", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "CmsUser",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR CmsUser_Id"),
                    Email = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    SurName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsUser", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "CmsUserPermission",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR CmsUserPermission_Id"),
                    ParentId = table.Column<int>(nullable: false),
                    Depth = table.Column<int>(nullable: false),
                    Resource = table.Column<string>(nullable: true),
                    PermissionOperation = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsUserPermission", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CmsUser_Id",
                schema: "security",
                table: "CmsUser",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CmsRole",
                schema: "security");

            migrationBuilder.DropTable(
                name: "CmsRolePermission",
                schema: "security");

            migrationBuilder.DropTable(
                name: "CmsUser",
                schema: "security");

            migrationBuilder.DropTable(
                name: "CmsUserPermission",
                schema: "security");

            migrationBuilder.DropSequence(
                name: "CmsRole_Id");

            migrationBuilder.DropSequence(
                name: "CmsRolePermission_Id");

            migrationBuilder.DropSequence(
                name: "CmsUser_Id");

            migrationBuilder.DropSequence(
                name: "CmsUserPermission_Id");
        }
    }
}
