using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class RenameChucVu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ChucVus",
                keyColumn: "Id",
                keyValue: 6,
                column: "TenChucVu",
                value: "NguoiPhuTrachCTĐT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ChucVus",
                keyColumn: "Id",
                keyValue: 6,
                column: "TenChucVu",
                value: "TruongBoMon");
        }
    }
}
