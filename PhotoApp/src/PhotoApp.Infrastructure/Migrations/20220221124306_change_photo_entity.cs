using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoApp.Infrastructure.Migrations
{
    public partial class change_photo_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_User_userEntityId",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_userEntityId",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "userEntityId",
                table: "Photo");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_UserId",
                table: "Photo",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "Fk_Photo_User",
                table: "Photo",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Fk_Photo_User",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Photo_UserId",
                table: "Photo");

            migrationBuilder.AddColumn<Guid>(
                name: "userEntityId",
                table: "Photo",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photo_userEntityId",
                table: "Photo",
                column: "userEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_User_userEntityId",
                table: "Photo",
                column: "userEntityId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
