using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.DAL.Migrations
{
    public partial class AspnetUserTypeJoin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetusertypjoin",
                columns: table => new
                {
                    IdAspNetUsers = table.Column<string>(nullable: false),
                    UsrTypID = table.Column<long>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetusertypjoin", x => new { x.IdAspNetUsers, x.UsrTypID });
                    table.ForeignKey(
                        name: "FK_AspNetusertypjoin_AspNetUsers_IdAspNetUsers",
                        column: x => x.IdAspNetUsers,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetusertypjoin_AspNetUsersTypes_UsrTypID",
                        column: x => x.UsrTypID,
                        principalTable: "AspNetUsersTypes",
                        principalColumn: "UsrTypID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetusertypjoin_UsrTypID",
                table: "AspNetusertypjoin",
                column: "UsrTypID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetusertypjoin");
        }
    }
}
