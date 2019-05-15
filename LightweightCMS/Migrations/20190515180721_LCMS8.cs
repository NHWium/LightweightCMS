using Microsoft.EntityFrameworkCore.Migrations;

namespace LightweightCMS.Migrations
{
    public partial class LCMS8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Element_Pages_PageId",
                table: "Element");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_AspNetUsers_UserId",
                table: "Pages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pages",
                table: "Pages");

            migrationBuilder.RenameTable(
                name: "Pages",
                newName: "Page");

            migrationBuilder.RenameIndex(
                name: "IX_Pages_UserId",
                table: "Page",
                newName: "IX_Page_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Page",
                table: "Page",
                column: "PageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Element_Page_PageId",
                table: "Element",
                column: "PageId",
                principalTable: "Page",
                principalColumn: "PageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Page_AspNetUsers_UserId",
                table: "Page",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Element_Page_PageId",
                table: "Element");

            migrationBuilder.DropForeignKey(
                name: "FK_Page_AspNetUsers_UserId",
                table: "Page");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Page",
                table: "Page");

            migrationBuilder.RenameTable(
                name: "Page",
                newName: "Pages");

            migrationBuilder.RenameIndex(
                name: "IX_Page_UserId",
                table: "Pages",
                newName: "IX_Pages_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pages",
                table: "Pages",
                column: "PageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Element_Pages_PageId",
                table: "Element",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "PageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_AspNetUsers_UserId",
                table: "Pages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
