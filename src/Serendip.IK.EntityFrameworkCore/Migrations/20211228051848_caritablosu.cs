using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class caritablosu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpsCurrent",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Unvan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ili_Id = table.Column<long>(type: "bigint", nullable: false),
                    Ilce_Id = table.Column<long>(type: "bigint", nullable: false),
                    AdresBul = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahalle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cadde = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BinaNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BinaAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Blok = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Daire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uyruk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SahisTuzel = table.Column<int>(type: "int", nullable: false),
                    ObjId = table.Column<long>(type: "bigint", nullable: false),
                    Kodu = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpsCurrent", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpsCurrent");
        }
    }
}
