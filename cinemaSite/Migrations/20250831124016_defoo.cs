using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cinemaSite.Migrations
{
    /// <inheritdoc />
    public partial class defoo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Unknown Movie",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "Unknown Movie");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Actor",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "Actor");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Unknown",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "Unknown");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Unknown Movie",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Unknown Movie");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Actor",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Actor");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Unknown",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Unknown");
        }
    }
}
