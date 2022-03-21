using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class EditSKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsOhter",
                table: "SKUnits",
                newName: "IsOther");

            migrationBuilder.RenameColumn(
                name: "IsOhter",
                table: "SKJobs",
                newName: "IsOther");

            migrationBuilder.RenameColumn(
                name: "IsOhter",
                table: "SKDepartments",
                newName: "IsOther");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsOther",
                table: "SKUnits",
                newName: "IsOhter");

            migrationBuilder.RenameColumn(
                name: "IsOther",
                table: "SKJobs",
                newName: "IsOhter");

            migrationBuilder.RenameColumn(
                name: "IsOther",
                table: "SKDepartments",
                newName: "IsOhter");
        }
    }
}
