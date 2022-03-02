using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class opkesi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpsInterruption",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kesintibirimi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kesintibirimkodu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kesintiyapilacakunvan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calismabaslangictarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Calismabitistarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kesintiorani = table.Column<int>(type: "int", nullable: false),
                    Tutar = table.Column<float>(type: "real", nullable: false),
                    TazminId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpsInterruption", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpsInterruption");
        }
    }
}
