using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherStation.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class New_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "WeatherLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WeatherName",
                table: "WeatherLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "WeatherLogs");

            migrationBuilder.DropColumn(
                name: "WeatherName",
                table: "WeatherLogs");
        }
    }
}
