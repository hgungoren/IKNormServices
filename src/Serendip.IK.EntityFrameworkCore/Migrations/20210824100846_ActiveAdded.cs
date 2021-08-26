using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class ActiveAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approve",
                table: "Nodes");

            migrationBuilder.RenameColumn(
                name: "Request",
                table: "Nodes",
                newName: "Active");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Nodes",
                newName: "Request");

            migrationBuilder.AddColumn<bool>(
                name: "Approve",
                table: "Nodes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
