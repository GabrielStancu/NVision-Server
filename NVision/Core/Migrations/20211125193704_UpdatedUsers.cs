using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class UpdatedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Watcher");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Subject");

            migrationBuilder.AddColumn<bool>(
                name: "IsPatient",
                table: "Subject",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPatient",
                table: "Subject");

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "Watcher",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "Subject",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
