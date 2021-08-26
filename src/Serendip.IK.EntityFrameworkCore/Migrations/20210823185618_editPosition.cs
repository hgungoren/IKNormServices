using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class editPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Units_UnitId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "BirimId",
                table: "Positions");

            migrationBuilder.AlterColumn<long>(
                name: "UnitId",
                table: "Positions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Units_UnitId",
                table: "Positions",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Units_UnitId",
                table: "Positions");

            migrationBuilder.AlterColumn<long>(
                name: "UnitId",
                table: "Positions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "BirimId",
                table: "Positions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Units_UnitId",
                table: "Positions",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
