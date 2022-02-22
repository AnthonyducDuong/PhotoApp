using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoApp.Infrastructure.Migrations
{
    public partial class updatereact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dislike");

            migrationBuilder.DropTable(
                name: "Like");

            migrationBuilder.CreateTable(
                name: "DislikePhoto",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DislikePhoto", x => new { x.UserId, x.PhotoId });
                    table.ForeignKey(
                        name: "Fk_DislikePhoto_Photo",
                        column: x => x.PhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Fk_DislikePhoto_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LikePhoto",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikePhoto", x => new { x.UserId, x.PhotoId });
                    table.ForeignKey(
                        name: "Fk_LikePhoto_Photo",
                        column: x => x.PhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Fk_LikePhoto_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DislikePhoto_PhotoId",
                table: "DislikePhoto",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_LikePhoto_PhotoId",
                table: "LikePhoto",
                column: "PhotoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DislikePhoto");

            migrationBuilder.DropTable(
                name: "LikePhoto");

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

            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => new { x.UserId, x.PhotoId });
                    table.ForeignKey(
                        name: "Fk_Like_Photo",
                        column: x => x.PhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Fk_Like_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dislike_PhotoId",
                table: "Dislike",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_PhotoId",
                table: "Like",
                column: "PhotoId");
        }
    }
}
