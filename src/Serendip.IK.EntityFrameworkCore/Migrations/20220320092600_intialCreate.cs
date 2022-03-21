using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class intialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComingPapers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GonderiTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BilgiHavale = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DosyaNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DefterNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TebligAlan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Konu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GonderilenYer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrjinalEvrakNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EvrakDurumu = table.Column<bool>(type: "bit", nullable: false),
                    EvrakTipi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComingPapers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComingPapers");
        }
    }
}
