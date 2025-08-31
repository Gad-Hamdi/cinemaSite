using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cinemaSite.Migrations
{
    /// <inheritdoc />
    public partial class first5araya : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "multiImageUrl",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "multiImageUrl",
                table: "Movies");
        }
    }
}
