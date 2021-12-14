using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class AddedWatcherToAlert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WatcherId",
                table: "Alert",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Alert_WatcherId",
                table: "Alert",
                column: "WatcherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alert_Watcher_WatcherId",
                table: "Alert",
                column: "WatcherId",
                principalTable: "Watcher",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alert_Watcher_WatcherId",
                table: "Alert");

            migrationBuilder.DropIndex(
                name: "IX_Alert_WatcherId",
                table: "Alert");

            migrationBuilder.DropColumn(
                name: "WatcherId",
                table: "Alert");
        }
    }
}
