using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class AddDamaCompeaV4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VarisSube_ObjId",
                table: "DamageCompensations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Surec_Sahibi_Birim_Bolge",
                table: "DamageCompensations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Odeme_Birimi_Bolge",
                table: "DamageCompensations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "IlkGondericiSube_ObjId",
                table: "DamageCompensations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Birimi_ObjId",
                table: "DamageCompensations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 100);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "VarisSube_ObjId",
                table: "DamageCompensations",
                type: "bigint",
                maxLength: 100,
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Surec_Sahibi_Birim_Bolge",
                table: "DamageCompensations",
                type: "bigint",
                maxLength: 100,
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Odeme_Birimi_Bolge",
                table: "DamageCompensations",
                type: "bigint",
                maxLength: 100,
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "IlkGondericiSube_ObjId",
                table: "DamageCompensations",
                type: "bigint",
                maxLength: 100,
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Birimi_ObjId",
                table: "DamageCompensations",
                type: "bigint",
                maxLength: 100,
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
