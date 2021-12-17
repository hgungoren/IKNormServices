using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class hthistroy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpsHistroy",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Islem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IslemYapanKullanici = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TazminStatu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OdemeDurumu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GmAciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BolgeAciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpsHistroy", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpsHistroy");
        }
    }
}
