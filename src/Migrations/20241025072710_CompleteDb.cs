using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class CompleteDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.CreateTable(
                name: "HocKies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocKies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Khoas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nganhs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nganhs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PLOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLOs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SinhViens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhViens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiangViens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KhoaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangViens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiangViens_Khoas_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "Khoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HocPhans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTinChi = table.Column<int>(type: "int", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTDTs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTinChi = table.Column<int>(type: "int", nullable: false),
                    KhoaId = table.Column<int>(type: "int", nullable: true),
                    NganhId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTDTs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CTDTs_Khoas_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "Khoas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CTDTs_Nganhs_NganhId",
                        column: x => x.NganhId,
                        principalTable: "Nganhs",
                        principalColumn: "Id");
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
                name: "LopHocPhans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HocKyId = table.Column<int>(type: "int", nullable: false),
                    HocPhanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHocPhans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LopHocPhans_HocKies_HocKyId",
                        column: x => x.HocKyId,
                        principalTable: "HocKies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LopHocPhans_HocPhans_HocPhanId",
                        column: x => x.HocPhanId,
                        principalTable: "HocPhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTDTHocPhan",
                columns: table => new
                {
                    CTDTsId = table.Column<int>(type: "int", nullable: false),
                    HocPhansId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTDTHocPhan", x => new { x.CTDTsId, x.HocPhansId });
                    table.ForeignKey(
                        name: "FK_CTDTHocPhan_CTDTs_CTDTsId",
                        column: x => x.CTDTsId,
                        principalTable: "CTDTs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CTDTHocPhan_HocPhans_HocPhansId",
                        column: x => x.HocPhansId,
                        principalTable: "HocPhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaiKiemTras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Loai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrongSo = table.Column<float>(type: "real", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CLOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LopHocPhanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLOs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CLOs_LopHocPhans_LopHocPhanId",
                        column: x => x.LopHocPhanId,
                        principalTable: "LopHocPhans",
                        principalColumn: "Id");
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
                name: "CauHois",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrongSo = table.Column<float>(type: "real", nullable: false),
                    BaiKiemTraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHois", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CauHois_BaiKiemTras_BaiKiemTraId",
                        column: x => x.BaiKiemTraId,
                        principalTable: "BaiKiemTras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Diem = table.Column<float>(type: "real", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KetQuas_SinhViens_SinhVienId",
                        column: x => x.SinhVienId,
                        principalTable: "SinhViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_CTDTHocPhan_HocPhansId",
                table: "CTDTHocPhan",
                column: "HocPhansId");

            migrationBuilder.CreateIndex(
                name: "IX_CTDTs_KhoaId",
                table: "CTDTs",
                column: "KhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_CTDTs_NganhId",
                table: "CTDTs",
                column: "NganhId");

            migrationBuilder.CreateIndex(
                name: "IX_GiangViens_KhoaId",
                table: "GiangViens",
                column: "KhoaId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLOCauHoi");

            migrationBuilder.DropTable(
                name: "CLOPLO");

            migrationBuilder.DropTable(
                name: "CTDTHocPhan");

            migrationBuilder.DropTable(
                name: "GiangViens");

            migrationBuilder.DropTable(
                name: "HocPhanPLO");

            migrationBuilder.DropTable(
                name: "KetQuas");

            migrationBuilder.DropTable(
                name: "LopHocPhanSinhVien");

            migrationBuilder.DropTable(
                name: "CLOs");

            migrationBuilder.DropTable(
                name: "CTDTs");

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
                name: "HocKies");

            migrationBuilder.DropTable(
                name: "HocPhans");

            migrationBuilder.DropTable(
                name: "Khoas");

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });
        }
    }
}
