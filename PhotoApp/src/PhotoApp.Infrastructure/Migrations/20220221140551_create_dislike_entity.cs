using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoApp.Infrastructure.Migrations
{
    public partial class create_dislike_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dislike",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dislike", x => new { x.UserId, x.PhotoId });
                    table.ForeignKey(
                        name: "Fk_Dislike_Photo",
                        column: x => x.PhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Fk_Dislike_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dislike_PhotoId",
                table: "Dislike",
                column: "PhotoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dislike");
        }
    }
}
