using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class UnitAndDeparment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DepartmentObjId",
                table: "IKPromotions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UnitObjId",
                table: "IKPromotions",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentObjId",
                table: "IKPromotions");

            migrationBuilder.DropColumn(
                name: "UnitObjId",
                table: "IKPromotions");
        }
    }
}
