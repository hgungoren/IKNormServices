using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class personelid3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TalepEdenPersonelId",
                table: "KNorm");

            migrationBuilder.AlterColumn<string>(
                name: "PersonelId",
                table: "KNorm",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "TelepTuru",
                table: "KNorm",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "YeniPozisyon",
                table: "KNorm",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TelepTuru",
                table: "KNorm");

            migrationBuilder.DropColumn(
                name: "YeniPozisyon",
                table: "KNorm");

            migrationBuilder.AlterColumn<long>(
                name: "PersonelId",
                table: "KNorm",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TalepEdenPersonelId",
                table: "KNorm",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
