using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherStation.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class Added_CityName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "WeatherLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "WeatherLogs");
        }
    }
}
