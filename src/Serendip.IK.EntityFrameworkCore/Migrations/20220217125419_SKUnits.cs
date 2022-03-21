using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class SKUnits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SKUnits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirimObjId = table.Column<long>(type: "bigint", nullable: false),
                    DepartmanObjId = table.Column<long>(type: "bigint", nullable: false),
                    Sistem_Timestamp = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aktif = table.Column<bool>(type: "bit", nullable: false),
                    IsOhter = table.Column<bool>(type: "bit", nullable: false),
                    Kodu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Listname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sirketi_ObjId = table.Column<long>(type: "bigint", nullable: false),
                    Sistem_InsertLogin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sistem_InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sistem_TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sistem_UpdateLogin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aciklama1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentKodu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sistem_UpdateTerminal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sistem_InsertTerminal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiraNo = table.Column<int>(type: "int", nullable: false),
                    BolgeHakki = table.Column<bool>(type: "bit", nullable: false),
                    MeslekKodu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeslekAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BordroKarsiligi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EksikGunNedeni_ObjId = table.Column<long>(type: "bigint", nullable: false),
                    MailSablonParametre = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKUnits", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SKUnits");
        }
    }
}
