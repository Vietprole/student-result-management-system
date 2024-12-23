using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDiemDinhChinh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CauHoiId",
                table: "DiemDinhChinhs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SinhVienId",
                table: "DiemDinhChinhs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DiemDinhChinhs_CauHoiId",
                table: "DiemDinhChinhs",
                column: "CauHoiId");

            migrationBuilder.CreateIndex(
                name: "IX_DiemDinhChinhs_NguoiDuyetId",
                table: "DiemDinhChinhs",
                column: "NguoiDuyetId");

            migrationBuilder.CreateIndex(
                name: "IX_DiemDinhChinhs_SinhVienId",
                table: "DiemDinhChinhs",
                column: "SinhVienId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiemDinhChinhs_CauHois_CauHoiId",
                table: "DiemDinhChinhs",
                column: "CauHoiId",
                principalTable: "CauHois",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiemDinhChinhs_SinhViens_SinhVienId",
                table: "DiemDinhChinhs",
                column: "SinhVienId",
                principalTable: "SinhViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiemDinhChinhs_TaiKhoans_NguoiDuyetId",
                table: "DiemDinhChinhs",
                column: "NguoiDuyetId",
                principalTable: "TaiKhoans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiemDinhChinhs_CauHois_CauHoiId",
                table: "DiemDinhChinhs");

            migrationBuilder.DropForeignKey(
                name: "FK_DiemDinhChinhs_SinhViens_SinhVienId",
                table: "DiemDinhChinhs");

            migrationBuilder.DropForeignKey(
                name: "FK_DiemDinhChinhs_TaiKhoans_NguoiDuyetId",
                table: "DiemDinhChinhs");

            migrationBuilder.DropIndex(
                name: "IX_DiemDinhChinhs_CauHoiId",
                table: "DiemDinhChinhs");

            migrationBuilder.DropIndex(
                name: "IX_DiemDinhChinhs_NguoiDuyetId",
                table: "DiemDinhChinhs");

            migrationBuilder.DropIndex(
                name: "IX_DiemDinhChinhs_SinhVienId",
                table: "DiemDinhChinhs");

            migrationBuilder.DropColumn(
                name: "CauHoiId",
                table: "DiemDinhChinhs");

            migrationBuilder.DropColumn(
                name: "SinhVienId",
                table: "DiemDinhChinhs");
        }
    }
}
