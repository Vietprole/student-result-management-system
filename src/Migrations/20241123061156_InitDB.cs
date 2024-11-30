using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d00d136-95d0-45f6-8f53-78de2c4c327d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "532abf13-061f-4b26-9084-bdd411d7036a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86060161-8994-4909-a802-113079d9b3d2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b31d1f30-df95-4d72-b349-e380671290f9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d954a44e-704c-48a9-9d85-a68c3ce01c3e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f66f8a66-7d0e-4d79-817b-09fc9098f78e");

            migrationBuilder.AddColumn<int>(
                name: "NamBatDau",
                table: "SinhViens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MaKhoa",
                table: "Khoas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VietTat",
                table: "Khoas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "05a64a12-4389-45d8-b821-b6e478d3c939", null, "GiangVien", "GIANGVIEN" },
                    { "3cea1710-2785-4f4b-bc8e-ba5dd6d9ab89", null, "TruongKhoa", "TRUONGKHOA" },
                    { "46f92d83-7fb8-4b3f-8bef-7744315be62a", null, "TruongBoMon", "TRUONGBOMON" },
                    { "4e4024f2-7402-4c3f-aa45-341a5202e4e1", null, "PhongDaoTao", "PHONGDAOTAO" },
                    { "94ec69c3-3f7f-42d9-8c98-9c7086001ebe", null, "SinhVien", "SINHVIEN" },
                    { "b2c5bdfa-2406-4d17-945e-f19654d88a73", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05a64a12-4389-45d8-b821-b6e478d3c939");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cea1710-2785-4f4b-bc8e-ba5dd6d9ab89");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46f92d83-7fb8-4b3f-8bef-7744315be62a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e4024f2-7402-4c3f-aa45-341a5202e4e1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94ec69c3-3f7f-42d9-8c98-9c7086001ebe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2c5bdfa-2406-4d17-945e-f19654d88a73");

            migrationBuilder.DropColumn(
                name: "NamBatDau",
                table: "SinhViens");

            migrationBuilder.DropColumn(
                name: "MaKhoa",
                table: "Khoas");

            migrationBuilder.DropColumn(
                name: "VietTat",
                table: "Khoas");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d00d136-95d0-45f6-8f53-78de2c4c327d", null, "TruongKhoa", "TRUONGKHOA" },
                    { "532abf13-061f-4b26-9084-bdd411d7036a", null, "GiangVien", "GIANGVIEN" },
                    { "86060161-8994-4909-a802-113079d9b3d2", null, "PhongDaoTao", "PHONGDAOTAO" },
                    { "b31d1f30-df95-4d72-b349-e380671290f9", null, "SinhVien", "SINHVIEN" },
                    { "d954a44e-704c-48a9-9d85-a68c3ce01c3e", null, "Admin", "ADMIN" },
                    { "f66f8a66-7d0e-4d79-817b-09fc9098f78e", null, "TruongBoMon", "TRUONGBOMON" }
                });
        }
    }
}
