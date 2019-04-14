using Microsoft.EntityFrameworkCore.Migrations;

namespace Borg.Platform.Backoffice.Security.EF.Data.Migrations
{
    public partial class third : Migration
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
                name: "CmsRolePermission",
                schema: "security",
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
                        principalSchema: "security",
                        principalTable: "CmsRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        principalSchema: "security",
                        principalTable: "CmsUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "security",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    CmsRoleId = table.Column<int>(nullable: true),
                    CmsUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.RoleId, x.UserId })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_UserRole_CmsRole_CmsRoleId",
                        column: x => x.CmsRoleId,
                        principalSchema: "security",
                        principalTable: "CmsRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_CmsUser_CmsUserId",
                        column: x => x.CmsUserId,
                        principalSchema: "security",
                        principalTable: "CmsUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CmsRolePermission_RoleId",
                schema: "security",
                table: "CmsRolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsUserPermission_UserId",
                schema: "security",
                table: "CmsUserPermission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_CmsRoleId",
                schema: "security",
                table: "UserRole",
                column: "CmsRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_CmsUserId",
                schema: "security",
                table: "UserRole",
                column: "CmsUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CmsRolePermission",
                schema: "security");

            migrationBuilder.DropTable(
                name: "CmsUserPermission",
                schema: "security");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "security");

            migrationBuilder.DropTable(
                name: "CmsRole",
                schema: "security");

            migrationBuilder.DropTable(
                name: "CmsUser",
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
