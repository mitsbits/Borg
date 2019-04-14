using Microsoft.EntityFrameworkCore.Migrations;

namespace Borg.Platform.Backoffice.Security.EF.Data.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_CmsRole_CmsRoleId",
                schema: "security",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_CmsUser_CmsUserId",
                schema: "security",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_CmsRoleId",
                schema: "security",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_CmsUserId",
                schema: "security",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "CmsRoleId",
                schema: "security",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "CmsUserId",
                schema: "security",
                table: "UserRole");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                schema: "security",
                table: "UserRole",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_CmsRole_RoleId",
                schema: "security",
                table: "UserRole",
                column: "RoleId",
                principalSchema: "security",
                principalTable: "CmsRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_CmsUser_UserId",
                schema: "security",
                table: "UserRole",
                column: "UserId",
                principalSchema: "security",
                principalTable: "CmsUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_CmsRole_RoleId",
                schema: "security",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_CmsUser_UserId",
                schema: "security",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_UserId",
                schema: "security",
                table: "UserRole");

            migrationBuilder.AddColumn<int>(
                name: "CmsRoleId",
                schema: "security",
                table: "UserRole",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CmsUserId",
                schema: "security",
                table: "UserRole",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_CmsRole_CmsRoleId",
                schema: "security",
                table: "UserRole",
                column: "CmsRoleId",
                principalSchema: "security",
                principalTable: "CmsRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_CmsUser_CmsUserId",
                schema: "security",
                table: "UserRole",
                column: "CmsUserId",
                principalSchema: "security",
                principalTable: "CmsUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
