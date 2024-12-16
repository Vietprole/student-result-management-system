using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class ModifyDataTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThoiGian",
                table: "DiemDinhChinhs",
                newName: "ThoiDiemMo");

            migrationBuilder.AddColumn<string>(
                name: "MaSinhVien",
                table: "SinhViens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaNganh",
                table: "Nganhs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "NguoiDuyetId",
                table: "DiemDinhChinhs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "ThoiDiemDuyet",
                table: "DiemDinhChinhs",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaSinhVien",
                table: "SinhViens");

            migrationBuilder.DropColumn(
                name: "MaNganh",
                table: "Nganhs");

            migrationBuilder.DropColumn(
                name: "ThoiDiemDuyet",
                table: "DiemDinhChinhs");

            migrationBuilder.RenameColumn(
                name: "ThoiDiemMo",
                table: "DiemDinhChinhs",
                newName: "ThoiGian");

            migrationBuilder.AlterColumn<int>(
                name: "NguoiDuyetId",
                table: "DiemDinhChinhs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
