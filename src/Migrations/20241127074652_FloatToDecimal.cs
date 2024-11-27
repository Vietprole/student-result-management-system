using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class FloatToDecimal : Migration
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

            migrationBuilder.AlterColumn<decimal>(
                name: "Diem",
                table: "KetQuas",
                type: "decimal(4,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "SoTinChi",
                table: "HocPhans",
                type: "decimal(4,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "TrongSo",
                table: "CauHois",
                type: "decimal(4,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "TrongSo",
                table: "BaiKiemTras",
                type: "decimal(4,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "42c4df42-f9a8-4cfb-a0cf-ac0c057e35f6", null, "SinhVien", "SINHVIEN" },
                    { "5c954f55-0a31-4efe-9ed7-6b4b1d50f1db", null, "TruongBoMon", "TRUONGBOMON" },
                    { "61f1c230-e5d8-4849-805c-727702559db8", null, "PhongDaoTao", "PHONGDAOTAO" },
                    { "6c85e1d6-6260-48a7-9944-daf87cdf0e34", null, "TruongKhoa", "TRUONGKHOA" },
                    { "780d21a8-3d32-4e7f-8ce1-550d3ff3a54a", null, "GiangVien", "GIANGVIEN" },
                    { "a77a7a4f-05d6-4c06-a406-297cf496489d", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42c4df42-f9a8-4cfb-a0cf-ac0c057e35f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c954f55-0a31-4efe-9ed7-6b4b1d50f1db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61f1c230-e5d8-4849-805c-727702559db8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c85e1d6-6260-48a7-9944-daf87cdf0e34");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "780d21a8-3d32-4e7f-8ce1-550d3ff3a54a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a77a7a4f-05d6-4c06-a406-297cf496489d");

            migrationBuilder.AlterColumn<float>(
                name: "Diem",
                table: "KetQuas",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)");

            migrationBuilder.AlterColumn<float>(
                name: "SoTinChi",
                table: "HocPhans",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)");

            migrationBuilder.AlterColumn<float>(
                name: "TrongSo",
                table: "CauHois",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)");

            migrationBuilder.AlterColumn<float>(
                name: "TrongSo",
                table: "BaiKiemTras",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)");

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
