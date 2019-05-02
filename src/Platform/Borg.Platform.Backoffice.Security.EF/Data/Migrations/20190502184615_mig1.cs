using Microsoft.EntityFrameworkCore.Migrations;

namespace Borg.Platform.Backoffice.Security.EF.Data.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR CmsRole_Id"),
                    Title = table.Column<string>(nullable: true),
                    IsSystem = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsRole", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "CmsUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR CmsUser_Id"),
                    Email = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsUser", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "CmsRolePermission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR CmsRolePermission_Id"),
                    ParentId = table.Column<int>(nullable: false),
                    Depth = table.Column<int>(nullable: false),
                    Resource = table.Column<string>(nullable: true),
                    PermissionOperation = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsRolePermission", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_CmsRolePermission_CmsRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "CmsRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CmsUserPermission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR CmsUserPermission_Id"),
                    ParentId = table.Column<int>(nullable: false),
                    Depth = table.Column<int>(nullable: false),
                    Resource = table.Column<string>(nullable: true),
                    PermissionOperation = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsUserPermission", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_CmsUserPermission_CmsUser_UserId",
                        column: x => x.UserId,
                        principalTable: "CmsUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.RoleId, x.UserId })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_UserRole_CmsRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "CmsRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_CmsUser_UserId",
                        column: x => x.UserId,
                        principalTable: "CmsUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CmsRolePermission_RoleId",
                table: "CmsRolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsUserPermission_UserId",
                table: "CmsUserPermission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CmsRolePermission");

            migrationBuilder.DropTable(
                name: "CmsUserPermission");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "CmsRole");

            migrationBuilder.DropTable(
                name: "CmsUser");

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