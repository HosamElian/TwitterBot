using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwitterBot.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNewsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Newses_AspNetUsers_ParaphrasedById",
                table: "Newses");

            migrationBuilder.DropIndex(
                name: "IX_Newses_ParaphrasedById",
                table: "Newses");

            migrationBuilder.DropColumn(
                name: "GetNewsTime",
                table: "Newses");

            migrationBuilder.DropColumn(
                name: "ParaphrasedById",
                table: "Newses");

            migrationBuilder.RenameColumn(
                name: "ParaphrasingTime",
                table: "Newses",
                newName: "CreatedTime");

            migrationBuilder.RenameColumn(
                name: "NewsSource",
                table: "Newses",
                newName: "UrlToImage");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Newses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Newses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedAt",
                table: "Newses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Newses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Newses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Newses");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Newses");

            migrationBuilder.DropColumn(
                name: "PublishedAt",
                table: "Newses");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Newses");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Newses");

            migrationBuilder.RenameColumn(
                name: "UrlToImage",
                table: "Newses",
                newName: "NewsSource");

            migrationBuilder.RenameColumn(
                name: "CreatedTime",
                table: "Newses",
                newName: "ParaphrasingTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "GetNewsTime",
                table: "Newses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ParaphrasedById",
                table: "Newses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Newses_ParaphrasedById",
                table: "Newses",
                column: "ParaphrasedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Newses_AspNetUsers_ParaphrasedById",
                table: "Newses",
                column: "ParaphrasedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
