using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Serendip.IK.Migrations
{
    public partial class personelid2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "KSube",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "KSube",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "KSube",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "KSube",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "KPersonel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "KPersonel",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "KPersonel",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "KPersonel",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "KNorm",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "KNorm",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "KNorm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "KNorm",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "KInkaLookUpTable",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "KInkaLookUpTable",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "KInkaLookUpTable",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "KInkaLookUpTable",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "KBolge",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "KBolge",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "KBolge",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "KBolge",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "KSube");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "KSube");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "KSube");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "KSube");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "KPersonel");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "KPersonel");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "KPersonel");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "KPersonel");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "KNorm");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "KNorm");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "KNorm");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "KNorm");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "KInkaLookUpTable");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "KInkaLookUpTable");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "KInkaLookUpTable");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "KInkaLookUpTable");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "KBolge");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "KBolge");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "KBolge");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "KBolge");
        }
    }
}
