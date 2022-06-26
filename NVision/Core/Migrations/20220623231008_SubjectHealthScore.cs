using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class SubjectHealthScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HealthScore",
                table: "Subject",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HealthScore",
                table: "Subject");
        }
    }
}
