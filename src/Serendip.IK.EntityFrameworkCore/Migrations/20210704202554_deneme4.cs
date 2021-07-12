using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class deneme4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NormSayisi",
                table: "KSube",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonelSayisi",
                table: "KSube",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToplamSayi",
                table: "KSube",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormSayisi",
                table: "KSube");

            migrationBuilder.DropColumn(
                name: "PersonelSayisi",
                table: "KSube");

            migrationBuilder.DropColumn(
                name: "ToplamSayi",
                table: "KSube");
        }
    }
}
