using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChucVu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ChucVus",
                columns: new[] { "Id", "TenChucVu" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "GiangVien" },
                    { 3, "SinhVien" },
                    { 4, "TruongKhoa" },
                    { 5, "PhongDaoTao" },
                    { 6, "TruongBoMon" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChucVus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ChucVus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ChucVus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ChucVus",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ChucVus",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ChucVus",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
