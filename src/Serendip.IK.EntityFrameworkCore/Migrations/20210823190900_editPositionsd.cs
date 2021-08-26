using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class editPositionsd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approve",
                table: "Nodes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Request",
                table: "Nodes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approve",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "Request",
                table: "Nodes");
        }
    }
}
