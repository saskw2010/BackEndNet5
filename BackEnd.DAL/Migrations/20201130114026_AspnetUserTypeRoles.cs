using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.DAL.Migrations
{
    public partial class AspnetUserTypeRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AspNetUsersTypes_roles",
                columns: table => new
                {
                    IdAspNetRoles = table.Column<string>(nullable: false),
                    UsrTypID = table.Column<long>(nullable: false),
                    IdentityRoleId = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsersTypes_roles", x => new { x.IdAspNetRoles, x.UsrTypID });
                    table.ForeignKey(
                        name: "FK_AspNetUsersTypes_roles_AspNetRoles_IdentityRoleId",
                        column: x => x.IdentityRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsersTypes_roles_AspNetUsersTypes_UsrTypID",
                        column: x => x.UsrTypID,
                        principalTable: "AspNetUsersTypes",
                        principalColumn: "UsrTypID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsersTypes_roles_IdentityRoleId",
                table: "AspNetUsersTypes_roles",
                column: "IdentityRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsersTypes_roles_UsrTypID",
                table: "AspNetUsersTypes_roles",
                column: "UsrTypID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetUsersTypes_roles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");
        }
    }
}
