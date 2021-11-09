using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class AddDamageCompensationV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "DamageCompensations",
                newName: "Varis_Sube_Unvan");

            migrationBuilder.AddColumn<float>(
                name: "Adet",
                table: "DamageCompensations",
                type: "real",
                maxLength: 100,
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "AliciKodu",
                table: "DamageCompensations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AliciUnvan",
                table: "DamageCompensations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Birimi",
                table: "DamageCompensations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Birimi_ObjId",
                table: "DamageCompensations",
                type: "bigint",
                maxLength: 100,
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Cikis_Sube_Unvan",
                table: "DamageCompensations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EvrakSeriNo",
                table: "DamageCompensations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GonderenKodu",
                table: "DamageCompensations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GonderenUnvan",
                table: "DamageCompensations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IlkGondericiSube_ObjId",
                table: "DamageCompensations",
                type: "bigint",
                maxLength: 100,
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "Sistem_InsertTime",
                table: "DamageCompensations",
                type: "datetime2",
                maxLength: 100,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TakipNo",
                table: "DamageCompensations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VarisSube_ObjId",
                table: "DamageCompensations",
                type: "bigint",
                maxLength: 100,
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adet",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "AliciKodu",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "AliciUnvan",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "Birimi",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "Birimi_ObjId",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "Cikis_Sube_Unvan",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "EvrakSeriNo",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "GonderenKodu",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "GonderenUnvan",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "IlkGondericiSube_ObjId",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "Sistem_InsertTime",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "TakipNo",
                table: "DamageCompensations");

            migrationBuilder.DropColumn(
                name: "VarisSube_ObjId",
                table: "DamageCompensations");

            migrationBuilder.RenameColumn(
                name: "Varis_Sube_Unvan",
                table: "DamageCompensations",
                newName: "Name");
        }
    }
}
