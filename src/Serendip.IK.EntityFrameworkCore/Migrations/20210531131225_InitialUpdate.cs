using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class InitialUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KBolge",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Tipi = table.Column<int>(type: "int", nullable: true),
                    TipTur = table.Column<int>(type: "int", nullable: true),
                    Aktif = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KBolge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KPersonel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObjId = table.Column<long>(type: "bigint", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aktif = table.Column<bool>(type: "bit", nullable: true),
                    IsYeri_ObjId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPersonel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KSube",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Tipi = table.Column<int>(type: "int", nullable: true),
                    TipTur = table.Column<int>(type: "int", nullable: true),
                    Aktif = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KSube", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KBolge");

            migrationBuilder.DropTable(
                name: "KPersonel");

            migrationBuilder.DropTable(
                name: "KSube");
        }
    }
}
