using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class hasaraddd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Durumu",
                table: "DamageCompensations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "KargoKabulFisNo",
                table: "DamageCompensations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Durumu",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "KargoKabulFisNo",
                table: "DamageCompensations");
        }
    }
}
