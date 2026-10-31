using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArmiVit.Migrations
{
    /// <inheritdoc />
    public partial class __ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutMeText2",
                table: "AboutPages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutMeText2",
                table: "AboutPages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
