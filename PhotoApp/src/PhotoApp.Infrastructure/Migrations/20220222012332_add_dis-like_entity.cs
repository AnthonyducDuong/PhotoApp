using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoApp.Infrastructure.Migrations
{
    public partial class add_dislike_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DislikeComment",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DislikeComment", x => new { x.UserId, x.CommentId });
                    table.ForeignKey(
                        name: "Fk_DislikeComment_Comment",
                        column: x => x.CommentId,
                        principalTable: "Comment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Fk_DislikeComment_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LikeComment",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeComment", x => new { x.UserId, x.CommentId });
                    table.ForeignKey(
                        name: "Fk_LikeComment_Comment",
                        column: x => x.CommentId,
                        principalTable: "Comment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Fk_LikeComment_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DislikeComment_CommentId",
                table: "DislikeComment",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeComment_CommentId",
                table: "LikeComment",
                column: "CommentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DislikeComment");

            migrationBuilder.DropTable(
                name: "LikeComment");
        }
    }
}
