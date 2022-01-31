using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class RemovedWatcherFromAlert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WatcherId",
                table: "Alert",
                type: "int",
                nullable: false,
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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
