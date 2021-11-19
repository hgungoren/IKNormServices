using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class AddDamagCopensoV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "DamageCompensations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Odeme_Birimi_Bolge",
                table: "DamageCompensations",
                type: "bigint",
                maxLength: 100,
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Odeme_Musteri_Tipi",
                table: "DamageCompensations",
                type: "int",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Surec_Sahibi_Birim_Bolge",
                table: "DamageCompensations",
                type: "bigint",
                maxLength: 100,
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "TCK_NO",
                table: "DamageCompensations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Talep_Edilen_Tutar",
                table: "DamageCompensations",
                type: "real",
                maxLength: 100,
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Tazmin_Musteri_Kodu",
                table: "DamageCompensations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tazmin_Musteri_Tipi",
                table: "DamageCompensations",
                type: "int",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tazmin_Musteri_Unvan",
                table: "DamageCompensations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Tazmin_Talep_Tarihi",
                table: "DamageCompensations",
                type: "datetime2",
                maxLength: 100,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Tazmin_Tipi",
                table: "DamageCompensations",
                type: "int",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "DamageCompensations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VK_NO",
                table: "DamageCompensations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "Odeme_Birimi_Bolge",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "Odeme_Musteri_Tipi",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "Surec_Sahibi_Birim_Bolge",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "TCK_NO",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "Talep_Edilen_Tutar",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "Tazmin_Musteri_Kodu",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "Tazmin_Musteri_Tipi",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "Tazmin_Musteri_Unvan",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "Tazmin_Talep_Tarihi",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "Tazmin_Tipi",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "VK_NO",
                table: "DamageCompensations");
        }
    }
}
