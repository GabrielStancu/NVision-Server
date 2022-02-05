using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class UserProfilePictureField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureSrc",
                table: "Watcher",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureSrc",
                table: "Subject",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureSrc",
                table: "Watcher");

            migrationBuilder.DropColumn(
                name: "ProfilePictureSrc",
                table: "Subject");
        }
    }
}
