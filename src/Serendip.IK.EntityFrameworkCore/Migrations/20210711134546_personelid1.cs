using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class personelid1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KNorm",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObjId = table.Column<long>(type: "bigint", nullable: false),
                    SubeObjId = table.Column<long>(type: "bigint", nullable: false),
                    TalepEdenPersonelId = table.Column<long>(type: "bigint", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pozisyon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TalepDurumu = table.Column<int>(type: "int", nullable: false),
                    TalepNedeni = table.Column<int>(type: "int", nullable: false),
                    PersonelId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KNorm", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KNorm");
        }
    }
}
