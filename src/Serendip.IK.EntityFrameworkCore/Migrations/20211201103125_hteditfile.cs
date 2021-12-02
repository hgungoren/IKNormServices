using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class hteditfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
       
            migrationBuilder.AddColumn<bool>(
                name: "DosyaActive",
                table: "DamageCompensationsFileInfos",
                type: "bit",
                maxLength: 100,
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DosyaTyp",
                table: "DamageCompensationsFileInfos",
                type: "int",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "DosyaActive",
                table: "DamageCompensationsFileInfos");

            migrationBuilder.DropColumn(
                name: "DosyaTyp",
                table: "DamageCompensationsFileInfos");
        }
    }
}
