using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class Addnewtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaiKhoanId",
                table: "SinhViens",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaiKhoanId",
                table: "GiangViens",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChucVu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChucVu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucVu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhanQuyens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenQuyen = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanQuyens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChucVuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaiKhoans_ChucVu_ChucVuId",
                        column: x => x.ChucVuId,
                        principalTable: "ChucVu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChucVuPhanQuyen",
                columns: table => new
                {
                    ChucVusId = table.Column<int>(type: "int", nullable: false),
                    PhanQuyensId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucVuPhanQuyen", x => new { x.ChucVusId, x.PhanQuyensId });
                    table.ForeignKey(
                        name: "FK_ChucVuPhanQuyen_ChucVu_ChucVusId",
                        column: x => x.ChucVusId,
                        principalTable: "ChucVu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChucVuPhanQuyen_PhanQuyens_PhanQuyensId",
                        column: x => x.PhanQuyensId,
                        principalTable: "PhanQuyens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_TaiKhoanId",
                table: "SinhViens",
                column: "TaiKhoanId");

            migrationBuilder.CreateIndex(
                name: "IX_GiangViens_TaiKhoanId",
                table: "GiangViens",
                column: "TaiKhoanId");

            migrationBuilder.CreateIndex(
                name: "IX_ChucVuPhanQuyen_PhanQuyensId",
                table: "ChucVuPhanQuyen",
                column: "PhanQuyensId");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_ChucVuId",
                table: "TaiKhoans",
                column: "ChucVuId");

            migrationBuilder.AddForeignKey(
                name: "FK_GiangViens_TaiKhoans_TaiKhoanId",
                table: "GiangViens",
                column: "TaiKhoanId",
                principalTable: "TaiKhoans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SinhViens_TaiKhoans_TaiKhoanId",
                table: "SinhViens",
                column: "TaiKhoanId",
                principalTable: "TaiKhoans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiangViens_TaiKhoans_TaiKhoanId",
                table: "GiangViens");

            migrationBuilder.DropForeignKey(
                name: "FK_SinhViens_TaiKhoans_TaiKhoanId",
                table: "SinhViens");

            migrationBuilder.DropTable(
                name: "ChucVuPhanQuyen");

            migrationBuilder.DropTable(
                name: "TaiKhoans");

            migrationBuilder.DropTable(
                name: "PhanQuyens");

            migrationBuilder.DropTable(
                name: "ChucVu");

            migrationBuilder.DropIndex(
                name: "IX_SinhViens_TaiKhoanId",
                table: "SinhViens");

            migrationBuilder.DropIndex(
                name: "IX_GiangViens_TaiKhoanId",
                table: "GiangViens");

            migrationBuilder.DropColumn(
                name: "TaiKhoanId",
                table: "SinhViens");

            migrationBuilder.DropColumn(
                name: "TaiKhoanId",
                table: "GiangViens");
        }
    }
}
