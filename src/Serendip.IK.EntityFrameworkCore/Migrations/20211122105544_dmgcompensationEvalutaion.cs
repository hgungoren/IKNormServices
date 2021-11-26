using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class dmgcompensationEvalutaion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TazminStatu",
                table: "DamageCompensations",
                type: "int",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "DamageCompensationEvaluations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TazminId = table.Column<long>(type: "bigint", maxLength: 100, nullable: false),
                    EvaTazmin_Tipi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EvaTazmin_Nedeni = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EvaKargo_Bulundugu_Yer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EvaKusurlu_Birim = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EvaIcerik_Grubu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EvaIcerik = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EvaUrun_Aciklama = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EvaEkleyen_Kullanici = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EvaBolge_Aciklama = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EvaGm_Aciklama = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EvaTalep_Edilen_Tutar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EvaTazmin_Odeme_Durumu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EvaOdenecek_Tutar = table.Column<float>(type: "real", maxLength: 100, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamageCompensationEvaluations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DamageCompensationsFileInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DamageCompensationId = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    DosyaAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DosyaYolu = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    DosyaUzantisi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamageCompensationsFileInfos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DamageCompensationEvaluations");

            migrationBuilder.DropTable(
                name: "DamageCompensationsFileInfos");

            migrationBuilder.AlterColumn<int>(
                name: "TazminStatu",
                table: "DamageCompensations",
                type: "int",
                maxLength: 100,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
