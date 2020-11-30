using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.DAL.Migrations
{
    public partial class AspnetUserTypeRoles3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsersTypes_roles_AspNetRoles_IdentityRoleId",
                table: "AspNetUsersTypes_roles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsersTypes_roles_IdentityRoleId",
                table: "AspNetUsersTypes_roles");

            migrationBuilder.DropColumn(
                name: "IdentityRoleId",
                table: "AspNetUsersTypes_roles");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsersTypes_roles_AspNetRoles_IdAspNetRoles",
                table: "AspNetUsersTypes_roles",
                column: "IdAspNetRoles",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsersTypes_roles_AspNetRoles_IdAspNetRoles",
                table: "AspNetUsersTypes_roles");

            migrationBuilder.AddColumn<string>(
                name: "IdentityRoleId",
                table: "AspNetUsersTypes_roles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsersTypes_roles_IdentityRoleId",
                table: "AspNetUsersTypes_roles",
                column: "IdentityRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsersTypes_roles_AspNetRoles_IdentityRoleId",
                table: "AspNetUsersTypes_roles",
                column: "IdentityRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
