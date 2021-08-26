using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class canTerminate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanTerminate",
                table: "Nodes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanTerminate",
                table: "Nodes");
        }
    }
}
