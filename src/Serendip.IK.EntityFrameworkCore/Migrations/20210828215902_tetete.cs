using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class tetete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "KNorm",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_KNorm_UserId",
                table: "KNorm",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_KNorm_AbpUsers_UserId",
                table: "KNorm",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KNorm_AbpUsers_UserId",
                table: "KNorm");

            migrationBuilder.DropIndex(
                name: "IX_KNorm_UserId",
                table: "KNorm");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "KNorm");
        }
    }
}
