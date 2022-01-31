using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class UnifiedSensorMeasurementsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "SensorMeasurement");

            migrationBuilder.AddColumn<string>(
                name: "SensorType",
                table: "SensorMeasurement",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "SensorMeasurement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.DropColumn(
                name: "SensorType",
                table: "SensorMeasurement");
        }
    }
}
