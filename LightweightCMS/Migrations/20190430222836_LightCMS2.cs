using Microsoft.EntityFrameworkCore.Migrations;

namespace LightweightCMS.Migrations
{
    public partial class LightCMS2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Pages",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Pages_UserId",
                table: "Pages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Elements_PageId",
                table: "Elements",
                column: "PageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Elements_Pages_PageId",
                table: "Elements",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "PageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_AspNetUsers_UserId",
                table: "Pages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Elements_Pages_PageId",
                table: "Elements");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_AspNetUsers_UserId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Pages_UserId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Elements_PageId",
                table: "Elements");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Pages",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
