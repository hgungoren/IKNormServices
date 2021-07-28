using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class knormDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TalepDurumu",
                table: "KNorm");

            migrationBuilder.CreateTable(
                name: "KNormDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KNormId = table.Column<long>(type: "bigint", nullable: false),
                    TalepDurumu = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KNormDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KNormDetails_KNorm_KNormId",
                        column: x => x.KNormId,
                        principalTable: "KNorm",
                        principalColumn: "ObjId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KNormDetails_KNormId",
                table: "KNormDetails",
                column: "KNormId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KNormDetails");

            migrationBuilder.AddColumn<int>(
                name: "TalepDurumu",
                table: "KNorm",
                type: "int",
                nullable: true);
        }
    }
}
