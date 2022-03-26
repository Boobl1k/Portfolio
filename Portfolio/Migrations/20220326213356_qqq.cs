using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Migrations
{
    public partial class qqq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Posts_PostsId",
                table: "PostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Tags_TagsId",
                table: "PostTag");

            migrationBuilder.RenameColumn(
                name: "TagsId",
                table: "PostTag",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "PostsId",
                table: "PostTag",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostTag_TagsId",
                table: "PostTag",
                newName: "IX_PostTag_TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Posts_PostId",
                table: "PostTag",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Tags_TagId",
                table: "PostTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Posts_PostId",
                table: "PostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Tags_TagId",
                table: "PostTag");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "PostTag",
                newName: "TagsId");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "PostTag",
                newName: "PostsId");

            migrationBuilder.RenameIndex(
                name: "IX_PostTag_TagId",
                table: "PostTag",
                newName: "IX_PostTag_TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Posts_PostsId",
                table: "PostTag",
                column: "PostsId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Tags_TagsId",
                table: "PostTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
