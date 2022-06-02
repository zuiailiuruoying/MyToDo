using Microsoft.EntityFrameworkCore.Migrations;

namespace MyToDo.Api.Migrations
{
    public partial class _20220602 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThemeColor",
                table: "User",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThemeColor",
                table: "User");
        }
    }
}
