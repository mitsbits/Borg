﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Borg.Platform.EF.Data.Migrations
{
    public partial class mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "CmsLanguage_Id");

            migrationBuilder.CreateSequence<int>(
                name: "CmsMenu_Id");

            migrationBuilder.CreateSequence<int>(
                name: "CmsMenuItem_Id");

            migrationBuilder.CreateSequence<int>(
                name: "CmsPage_Id");

            migrationBuilder.CreateSequence<int>(
                name: "CmsRole_Id");

            migrationBuilder.CreateSequence<int>(
                name: "CmsRolePermission_Id");

            migrationBuilder.CreateSequence<int>(
                name: "CmsUser_Id");

            migrationBuilder.CreateSequence<int>(
                name: "CmsUserPermission_Id");

            migrationBuilder.CreateTable(
                name: "CmsLanguage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR CmsLanguage_Id"),
                    Title = table.Column<string>(nullable: true),
                    TwoLetterISO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsLanguage", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

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
                name: "CmsMenu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR CmsMenu_Id"),
                    TwoLetterISO = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsMenu", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_CmsMenu_CmsLanguage_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "CmsLanguage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CmsPage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR CmsPage_Id"),
                    TwoLetterISO = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: true),
                    ParentId = table.Column<int>(nullable: false),
                    Depth = table.Column<int>(nullable: false),
                    Hierarchy = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsPage", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_CmsPage_CmsLanguage_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "CmsLanguage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CmsRolePermission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR CmsRolePermission_Id"),
                    ParentId = table.Column<int>(nullable: false),
                    Depth = table.Column<int>(nullable: false),
                    Hierarchy = table.Column<string>(nullable: true),
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
                name: "CmsUserCmsRole",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsUserCmsRole", x => new { x.RoleId, x.UserId })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_CmsUserCmsRole_CmsRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "CmsRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CmsUserCmsRole_CmsUser_UserId",
                        column: x => x.UserId,
                        principalTable: "CmsUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CmsUserPermission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR CmsUserPermission_Id"),
                    ParentId = table.Column<int>(nullable: false),
                    Depth = table.Column<int>(nullable: false),
                    Hierarchy = table.Column<string>(nullable: true),
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
                name: "CmsMenuItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR CmsMenuItem_Id"),
                    TwoLetterISO = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: true),
                    ParentId = table.Column<int>(nullable: false),
                    Depth = table.Column<int>(nullable: false),
                    Hierarchy = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    MenuId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsMenuItem", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_CmsMenuItem_CmsLanguage_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "CmsLanguage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CmsMenuItem_CmsMenu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "CmsMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IndexName",
                table: "CmsLanguage",
                column: "TwoLetterISO",
                unique: true,
                filter: "[TwoLetterISO] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CmsMenu_LanguageId",
                table: "CmsMenu",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsMenuItem_LanguageId",
                table: "CmsMenuItem",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsMenuItem_MenuId",
                table: "CmsMenuItem",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsPage_LanguageId",
                table: "CmsPage",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsRolePermission_RoleId",
                table: "CmsRolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsUserCmsRole_UserId",
                table: "CmsUserCmsRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsUserPermission_UserId",
                table: "CmsUserPermission",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CmsMenuItem");

            migrationBuilder.DropTable(
                name: "CmsPage");

            migrationBuilder.DropTable(
                name: "CmsRolePermission");

            migrationBuilder.DropTable(
                name: "CmsUserCmsRole");

            migrationBuilder.DropTable(
                name: "CmsUserPermission");

            migrationBuilder.DropTable(
                name: "CmsMenu");

            migrationBuilder.DropTable(
                name: "CmsRole");

            migrationBuilder.DropTable(
                name: "CmsUser");

            migrationBuilder.DropTable(
                name: "CmsLanguage");

            migrationBuilder.DropSequence(
                name: "CmsLanguage_Id");

            migrationBuilder.DropSequence(
                name: "CmsMenu_Id");

            migrationBuilder.DropSequence(
                name: "CmsMenuItem_Id");

            migrationBuilder.DropSequence(
                name: "CmsPage_Id");

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