using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArmiVit.Migrations
{
    /// <inheritdoc />
    public partial class AddNevParameterExpirienceText1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExpirienceText1",
                table: "AboutMePage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirienceText1",
                table: "AboutMePage");
        }
    }
}
