using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aladang_server_api.Migrations
{
    public partial class appUserAuthorizes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "appUserAuthorizes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expireddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userStatus = table.Column<bool>(type: "bit", nullable: false),
                    createby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appUserAuthorizes", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appUserAuthorizes");
        }
    }
}
