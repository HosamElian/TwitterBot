using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwitterBot.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNewsTable4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExpired",
                table: "Newses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExpired",
                table: "Newses");
        }
    }
}
