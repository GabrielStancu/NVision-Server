using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class NullableWatcherIdOnSubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Watcher_WatcherId",
                table: "Subject");

            migrationBuilder.AlterColumn<int>(
                name: "WatcherId",
                table: "Subject",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Watcher_WatcherId",
                table: "Subject",
                column: "WatcherId",
                principalTable: "Watcher",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Watcher_WatcherId",
                table: "Subject");

            migrationBuilder.AlterColumn<int>(
                name: "WatcherId",
                table: "Subject",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Watcher_WatcherId",
                table: "Subject",
                column: "WatcherId",
                principalTable: "Watcher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
