using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Borg.Platform.EF.Data.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "borgdb");

            migrationBuilder.CreateSequence<int>(
                name: "CmsLanguage_Id_seq");

            migrationBuilder.CreateSequence<int>(
                name: "CmsMenu_Id_seq");

            migrationBuilder.CreateSequence<int>(
                name: "CmsMenuItem_Id_seq");

            migrationBuilder.CreateSequence<int>(
                name: "CmsPage_Id_seq");

            migrationBuilder.CreateSequence<int>(
                name: "CmsUser_Id_seq");

            migrationBuilder.CreateTable(
                name: "CmsLanguage",
                schema: "borgdb",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR CmsLanguage_Id_seq"),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    TwoLetterISO = table.Column<string>(maxLength: 100, nullable: true),
                    CultureName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsLanguage_Id", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "CmsRole",
                schema: "borgdb",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    IsSystem = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CmsUser",
                schema: "borgdb",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR CmsUser_Id_seq"),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    PasswordHash = table.Column<string>(maxLength: 100, nullable: true),
                    Surname = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsUser_Id", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "CmsMenu",
                schema: "borgdb",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR CmsMenu_Id_seq"),
                    LanguageID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsMenu_Id_LanguageID", x => new { x.Id, x.LanguageID })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_CmsMenu_CmsLanguage_LanguageID",
                        column: x => x.LanguageID,
                        principalSchema: "borgdb",
                        principalTable: "CmsLanguage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CmsPage",
                schema: "borgdb",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR CmsPage_Id_seq"),
                    LanguageID = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: false),
                    Depth = table.Column<int>(nullable: false),
                    Hierarchy = table.Column<string>(maxLength: 100, nullable: true),
                    Title = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsPage_Id_LanguageID", x => new { x.Id, x.LanguageID })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_CmsPage_CmsLanguage_LanguageID",
                        column: x => x.LanguageID,
                        principalSchema: "borgdb",
                        principalTable: "CmsLanguage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CmsRolePermission",
                schema: "borgdb",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentId = table.Column<int>(nullable: false),
                    Depth = table.Column<int>(nullable: false),
                    Hierarchy = table.Column<string>(maxLength: 100, nullable: true),
                    Resource = table.Column<string>(maxLength: 100, nullable: true),
                    PermissionOperation = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsRolePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsRolePermission_CmsRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "borgdb",
                        principalTable: "CmsRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CmsUserCmsRole",
                schema: "borgdb",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole_UserId_RoleId", x => new { x.UserId, x.RoleId })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_CmsUserCmsRole_CmsRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "borgdb",
                        principalTable: "CmsRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CmsUserCmsRole_CmsUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "borgdb",
                        principalTable: "CmsUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CmsUserPermission",
                schema: "borgdb",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentId = table.Column<int>(nullable: false),
                    Depth = table.Column<int>(nullable: false),
                    Hierarchy = table.Column<string>(maxLength: 100, nullable: true),
                    Resource = table.Column<string>(maxLength: 100, nullable: true),
                    PermissionOperation = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsUserPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsUserPermission_CmsUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "borgdb",
                        principalTable: "CmsUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CmsMenuItem",
                schema: "borgdb",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR CmsMenuItem_Id_seq"),
                    LanguageID = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: false),
                    Depth = table.Column<int>(nullable: false),
                    Hierarchy = table.Column<string>(maxLength: 100, nullable: true),
                    Title = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    MenuId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsMenuItem_Id_LanguageID", x => new { x.Id, x.LanguageID })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_CmsMenuItem_CmsLanguage_LanguageID",
                        column: x => x.LanguageID,
                        principalSchema: "borgdb",
                        principalTable: "CmsLanguage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CmsMenuItem_CmsMenu_MenuId_LanguageID",
                        columns: x => new { x.MenuId, x.LanguageID },
                        principalSchema: "borgdb",
                        principalTable: "CmsMenu",
                        principalColumns: new[] { "Id", "LanguageID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TwoLetterISO",
                schema: "borgdb",
                table: "CmsLanguage",
                column: "TwoLetterISO");

            migrationBuilder.CreateIndex(
                name: "IX_CmsMenu_Id",
                schema: "borgdb",
                table: "CmsMenu",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CmsMenu_LanguageID",
                schema: "borgdb",
                table: "CmsMenu",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_CmsMenuItem_LanguageID",
                schema: "borgdb",
                table: "CmsMenuItem",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_CmsMenuItem_MenuId_LanguageID",
                schema: "borgdb",
                table: "CmsMenuItem",
                columns: new[] { "MenuId", "LanguageID" });

            migrationBuilder.CreateIndex(
                name: "IX_CmsPage_LanguageID",
                schema: "borgdb",
                table: "CmsPage",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_CmsRolePermission_RoleId",
                schema: "borgdb",
                table: "CmsRolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsUserCmsRole_RoleId",
                schema: "borgdb",
                table: "CmsUserCmsRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsUserPermission_UserId",
                schema: "borgdb",
                table: "CmsUserPermission",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CmsMenuItem",
                schema: "borgdb");

            migrationBuilder.DropTable(
                name: "CmsPage",
                schema: "borgdb");

            migrationBuilder.DropTable(
                name: "CmsRolePermission",
                schema: "borgdb");

            migrationBuilder.DropTable(
                name: "CmsUserCmsRole",
                schema: "borgdb");

            migrationBuilder.DropTable(
                name: "CmsUserPermission",
                schema: "borgdb");

            migrationBuilder.DropTable(
                name: "CmsMenu",
                schema: "borgdb");

            migrationBuilder.DropTable(
                name: "CmsRole",
                schema: "borgdb");

            migrationBuilder.DropTable(
                name: "CmsUser",
                schema: "borgdb");

            migrationBuilder.DropTable(
                name: "CmsLanguage",
                schema: "borgdb");

            migrationBuilder.DropSequence(
                name: "CmsLanguage_Id_seq");

            migrationBuilder.DropSequence(
                name: "CmsMenu_Id_seq");

            migrationBuilder.DropSequence(
                name: "CmsMenuItem_Id_seq");

            migrationBuilder.DropSequence(
                name: "CmsPage_Id_seq");

            migrationBuilder.DropSequence(
                name: "CmsUser_Id_seq");
        }
    }
}