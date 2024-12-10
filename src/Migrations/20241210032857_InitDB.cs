using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChucVus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChucVu = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucVus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HocKies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NamHoc = table.Column<int>(type: "int", nullable: false),
                    MaHocKy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocKies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    ChucVuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaiKhoans_ChucVus_ChucVuId",
                        column: x => x.ChucVuId,
                        principalTable: "ChucVus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Khoas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaKhoa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TruongKhoaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Khoas_TaiKhoans_TruongKhoaId",
                        column: x => x.TruongKhoaId,
                        principalTable: "TaiKhoans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GiangViens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaGiangVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KhoaId = table.Column<int>(type: "int", nullable: true),
                    TaiKhoanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangViens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiangViens_Khoas_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "Khoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GiangViens_TaiKhoans_TaiKhoanId",
                        column: x => x.TaiKhoanId,
                        principalTable: "TaiKhoans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "HocPhans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHocPhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTinChi = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    LaCotLoi = table.Column<bool>(type: "bit", nullable: false),
                    KhoaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocPhans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HocPhans_Khoas_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "Khoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nganhs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KhoaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nganhs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nganhs_Khoas_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "Khoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SinhViens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhoaNhapHoc = table.Column<int>(type: "int", nullable: false),
                    KhoaId = table.Column<int>(type: "int", nullable: false),
                    TaiKhoanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhViens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SinhViens_Khoas_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "Khoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SinhViens_TaiKhoans_TaiKhoanId",
                        column: x => x.TaiKhoanId,
                        principalTable: "TaiKhoans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "LopHocPhans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaLopHocPhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HocPhanId = table.Column<int>(type: "int", nullable: false),
                    HocKyId = table.Column<int>(type: "int", nullable: false),
                    HanDeXuatCongThucDiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiDeXuatCongThucDiemId = table.Column<int>(type: "int", nullable: true),
                    NguoiChapNhanCongThucDiemId = table.Column<int>(type: "int", nullable: true),
                    NgayChapNhanCongThucDiem = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GiangVienId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHocPhans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LopHocPhans_GiangViens_GiangVienId",
                        column: x => x.GiangVienId,
                        principalTable: "GiangViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LopHocPhans_HocKies_HocKyId",
                        column: x => x.HocKyId,
                        principalTable: "HocKies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LopHocPhans_HocPhans_HocPhanId",
                        column: x => x.HocPhanId,
                        principalTable: "HocPhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HocPhanNganh",
                columns: table => new
                {
                    HocPhansId = table.Column<int>(type: "int", nullable: false),
                    NganhsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocPhanNganh", x => new { x.HocPhansId, x.NganhsId });
                    table.ForeignKey(
                        name: "FK_HocPhanNganh_HocPhans_HocPhansId",
                        column: x => x.HocPhansId,
                        principalTable: "HocPhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HocPhanNganh_Nganhs_NganhsId",
                        column: x => x.NganhsId,
                        principalTable: "Nganhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PLOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NganhId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLOs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PLOs_Nganhs_NganhId",
                        column: x => x.NganhId,
                        principalTable: "Nganhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BaiKiemTras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Loai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrongSo = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TrongSoDeXuat = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    NgayMoNhapDiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HanNhapDiem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HanDinhChinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayXacNhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LopHocPhanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaiKiemTras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaiKiemTras_LopHocPhans_LopHocPhanId",
                        column: x => x.LopHocPhanId,
                        principalTable: "LopHocPhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CLOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LopHocPhanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLOs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CLOs_LopHocPhans_LopHocPhanId",
                        column: x => x.LopHocPhanId,
                        principalTable: "LopHocPhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LopHocPhanSinhVien",
                columns: table => new
                {
                    LopHocPhansId = table.Column<int>(type: "int", nullable: false),
                    SinhViensId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHocPhanSinhVien", x => new { x.LopHocPhansId, x.SinhViensId });
                    table.ForeignKey(
                        name: "FK_LopHocPhanSinhVien_LopHocPhans_LopHocPhansId",
                        column: x => x.LopHocPhansId,
                        principalTable: "LopHocPhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LopHocPhanSinhVien_SinhViens_SinhViensId",
                        column: x => x.SinhViensId,
                        principalTable: "SinhViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HocPhanPLO",
                columns: table => new
                {
                    HocPhansId = table.Column<int>(type: "int", nullable: false),
                    PLOsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocPhanPLO", x => new { x.HocPhansId, x.PLOsId });
                    table.ForeignKey(
                        name: "FK_HocPhanPLO_HocPhans_HocPhansId",
                        column: x => x.HocPhansId,
                        principalTable: "HocPhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HocPhanPLO_PLOs_PLOsId",
                        column: x => x.PLOsId,
                        principalTable: "PLOs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CauHois",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrongSo = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    BaiKiemTraId = table.Column<int>(type: "int", nullable: false),
                    ThangDiem = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHois", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CauHois_BaiKiemTras_BaiKiemTraId",
                        column: x => x.BaiKiemTraId,
                        principalTable: "BaiKiemTras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CLOPLO",
                columns: table => new
                {
                    CLOsId = table.Column<int>(type: "int", nullable: false),
                    PLOsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLOPLO", x => new { x.CLOsId, x.PLOsId });
                    table.ForeignKey(
                        name: "FK_CLOPLO_CLOs_CLOsId",
                        column: x => x.CLOsId,
                        principalTable: "CLOs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CLOPLO_PLOs_PLOsId",
                        column: x => x.PLOsId,
                        principalTable: "PLOs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CLOCauHoi",
                columns: table => new
                {
                    CLOsId = table.Column<int>(type: "int", nullable: false),
                    CauHoisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLOCauHoi", x => new { x.CLOsId, x.CauHoisId });
                    table.ForeignKey(
                        name: "FK_CLOCauHoi_CLOs_CLOsId",
                        column: x => x.CLOsId,
                        principalTable: "CLOs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CLOCauHoi_CauHois_CauHoisId",
                        column: x => x.CauHoisId,
                        principalTable: "CauHois",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KetQuas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiemTam = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    DiemChinhThuc = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    DaCongBo = table.Column<bool>(type: "bit", nullable: false),
                    DaXacNhan = table.Column<bool>(type: "bit", nullable: false),
                    SinhVienId = table.Column<int>(type: "int", nullable: false),
                    CauHoiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KetQuas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KetQuas_CauHois_CauHoiId",
                        column: x => x.CauHoiId,
                        principalTable: "CauHois",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KetQuas_SinhViens_SinhVienId",
                        column: x => x.SinhVienId,
                        principalTable: "SinhViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiemDinhChinhs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KetQuaId = table.Column<int>(type: "int", nullable: false),
                    DiemMoi = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuocDuyet = table.Column<bool>(type: "bit", nullable: false),
                    NguoiDuyetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiemDinhChinhs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiemDinhChinhs_KetQuas_KetQuaId",
                        column: x => x.KetQuaId,
                        principalTable: "KetQuas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaiKiemTras_LopHocPhanId",
                table: "BaiKiemTras",
                column: "LopHocPhanId");

            migrationBuilder.CreateIndex(
                name: "IX_CauHois_BaiKiemTraId",
                table: "CauHois",
                column: "BaiKiemTraId");

            migrationBuilder.CreateIndex(
                name: "IX_CLOCauHoi_CauHoisId",
                table: "CLOCauHoi",
                column: "CauHoisId");

            migrationBuilder.CreateIndex(
                name: "IX_CLOPLO_PLOsId",
                table: "CLOPLO",
                column: "PLOsId");

            migrationBuilder.CreateIndex(
                name: "IX_CLOs_LopHocPhanId",
                table: "CLOs",
                column: "LopHocPhanId");

            migrationBuilder.CreateIndex(
                name: "IX_DiemDinhChinhs_KetQuaId",
                table: "DiemDinhChinhs",
                column: "KetQuaId");

            migrationBuilder.CreateIndex(
                name: "IX_GiangViens_KhoaId",
                table: "GiangViens",
                column: "KhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_GiangViens_TaiKhoanId",
                table: "GiangViens",
                column: "TaiKhoanId",
                unique: true,
                filter: "[TaiKhoanId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HocPhanNganh_NganhsId",
                table: "HocPhanNganh",
                column: "NganhsId");

            migrationBuilder.CreateIndex(
                name: "IX_HocPhanPLO_PLOsId",
                table: "HocPhanPLO",
                column: "PLOsId");

            migrationBuilder.CreateIndex(
                name: "IX_HocPhans_KhoaId",
                table: "HocPhans",
                column: "KhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_KetQuas_CauHoiId",
                table: "KetQuas",
                column: "CauHoiId");

            migrationBuilder.CreateIndex(
                name: "IX_KetQuas_SinhVienId",
                table: "KetQuas",
                column: "SinhVienId");

            migrationBuilder.CreateIndex(
                name: "IX_Khoas_TruongKhoaId",
                table: "Khoas",
                column: "TruongKhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_LopHocPhans_GiangVienId",
                table: "LopHocPhans",
                column: "GiangVienId");

            migrationBuilder.CreateIndex(
                name: "IX_LopHocPhans_HocKyId",
                table: "LopHocPhans",
                column: "HocKyId");

            migrationBuilder.CreateIndex(
                name: "IX_LopHocPhans_HocPhanId",
                table: "LopHocPhans",
                column: "HocPhanId");

            migrationBuilder.CreateIndex(
                name: "IX_LopHocPhanSinhVien_SinhViensId",
                table: "LopHocPhanSinhVien",
                column: "SinhViensId");

            migrationBuilder.CreateIndex(
                name: "IX_Nganhs_KhoaId",
                table: "Nganhs",
                column: "KhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_PLOs_NganhId",
                table: "PLOs",
                column: "NganhId");

            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_KhoaId",
                table: "SinhViens",
                column: "KhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_TaiKhoanId",
                table: "SinhViens",
                column: "TaiKhoanId",
                unique: true,
                filter: "[TaiKhoanId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_ChucVuId",
                table: "TaiKhoans",
                column: "ChucVuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLOCauHoi");

            migrationBuilder.DropTable(
                name: "CLOPLO");

            migrationBuilder.DropTable(
                name: "DiemDinhChinhs");

            migrationBuilder.DropTable(
                name: "HocPhanNganh");

            migrationBuilder.DropTable(
                name: "HocPhanPLO");

            migrationBuilder.DropTable(
                name: "LopHocPhanSinhVien");

            migrationBuilder.DropTable(
                name: "CLOs");

            migrationBuilder.DropTable(
                name: "KetQuas");

            migrationBuilder.DropTable(
                name: "PLOs");

            migrationBuilder.DropTable(
                name: "CauHois");

            migrationBuilder.DropTable(
                name: "SinhViens");

            migrationBuilder.DropTable(
                name: "Nganhs");

            migrationBuilder.DropTable(
                name: "BaiKiemTras");

            migrationBuilder.DropTable(
                name: "LopHocPhans");

            migrationBuilder.DropTable(
                name: "GiangViens");

            migrationBuilder.DropTable(
                name: "HocKies");

            migrationBuilder.DropTable(
                name: "HocPhans");

            migrationBuilder.DropTable(
                name: "Khoas");

            migrationBuilder.DropTable(
                name: "TaiKhoans");

            migrationBuilder.DropTable(
                name: "ChucVus");
        }
    }
}
