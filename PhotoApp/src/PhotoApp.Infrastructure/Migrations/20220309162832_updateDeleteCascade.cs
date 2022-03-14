using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoApp.Infrastructure.Migrations
{
    public partial class updateDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Fk_Comment_Comment",
                table: "Comment");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5227d0bc-94de-4b4b-a30e-d9c99e587dda"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("892ac795-3f72-4ea8-88e3-138b41eec490"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("217c0c4d-a1f3-4934-8480-af4235669fcd"), "2", "USER", "User" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("99d1ac85-25cf-433d-b830-2911bd65a529"), "1", "ADMIN", "Admin" });

            migrationBuilder.AddForeignKey(
                name: "Fk_Comment_Comment",
                table: "Comment",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Fk_Comment_Comment",
                table: "Comment");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("217c0c4d-a1f3-4934-8480-af4235669fcd"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("99d1ac85-25cf-433d-b830-2911bd65a529"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("5227d0bc-94de-4b4b-a30e-d9c99e587dda"), "2", "USER", "User" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("892ac795-3f72-4ea8-88e3-138b41eec490"), "1", "ADMIN", "Admin" });

            migrationBuilder.AddForeignKey(
                name: "Fk_Comment_Comment",
                table: "Comment",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "Id");
        }
    }
}
