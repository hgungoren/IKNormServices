using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class NormEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Posizyon",
                table: "KSubeNorm",
                newName: "Pozisyon");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pozisyon",
                table: "KSubeNorm",
                newName: "Posizyon");
        }
    }
}
