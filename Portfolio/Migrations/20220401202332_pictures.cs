using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Migrations
{
    public partial class pictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PictureId",
                table: "AspNetUsers",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Picture_PictureId",
                table: "AspNetUsers",
                column: "PictureId",
                principalTable: "Picture",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Picture_PictureId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PictureId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "AspNetUsers");
        }
    }
}
