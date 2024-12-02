using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Student_Result_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HovaTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Khoas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaKhoa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VietTat = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GiangViens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhoaId = table.Column<int>(type: "int", nullable: true),
                    TaiKhoanId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangViens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiangViens_AspNetUsers_TaiKhoanId",
                        column: x => x.TaiKhoanId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GiangViens_Khoas_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "Khoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "HocPhans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTinChi = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    LaCotLoi = table.Column<bool>(type: "bit", nullable: false),
                    KhoaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocPhans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HocPhans_Khoas_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "Khoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Nganhs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KhoaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nganhs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nganhs_Khoas_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "Khoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SinhViens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamBatDau = table.Column<int>(type: "int", nullable: false),
                    KhoaId = table.Column<int>(type: "int", nullable: true),
                    TaiKhoanId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhViens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SinhViens_AspNetUsers_TaiKhoanId",
                        column: x => x.TaiKhoanId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SinhViens_Khoas_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "Khoas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LopHocPhans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HocPhanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHocPhans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LopHocPhans_HocPhans_HocPhanId",
                        column: x => x.HocPhanId,
                        principalTable: "HocPhans",
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
                    NganhId = table.Column<int>(type: "int", nullable: true),
                    KhoaId = table.Column<int>(type: "int", nullable: true)
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "BaiKiemTras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Loai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrongSo = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GiangVienLopHocPhan",
                columns: table => new
                {
                    GiangViensId = table.Column<int>(type: "int", nullable: false),
                    LopHocPhansId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangVienLopHocPhan", x => new { x.GiangViensId, x.LopHocPhansId });
                    table.ForeignKey(
                        name: "FK_GiangVienLopHocPhan_GiangViens_GiangViensId",
                        column: x => x.GiangViensId,
                        principalTable: "GiangViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GiangVienLopHocPhan_LopHocPhans_LopHocPhansId",
                        column: x => x.LopHocPhansId,
                        principalTable: "LopHocPhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "PLOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CTDTId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLOs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PLOs_CTDTs_CTDTId",
                        column: x => x.CTDTId,
                        principalTable: "CTDTs",
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
                    TrongSo = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
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
                        principalColumn: "Id");
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
                    Diem = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f9f5597-22c0-4d86-9f6a-3c8d0505324e", null, "TruongBoMon", "TRUONGBOMON" },
                    { "1b3de155-3a7e-4e19-8b27-93e7a4ab91d8", null, "PhongDaoTao", "PHONGDAOTAO" },
                    { "297a4ced-a670-466e-89a1-868e171d2c90", null, "Admin", "ADMIN" },
                    { "8168c32a-e115-43b3-9a75-e58b56121d44", null, "GiangVien", "GIANGVIEN" },
                    { "834cdf2b-9528-4eac-b607-71aff7ed673c", null, "SinhVien", "SINHVIEN" },
                    { "ad2d2e3d-fe41-4960-88be-cc197a6fd249", null, "TruongKhoa", "TRUONGKHOA" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "IX_GiangVienLopHocPhan_LopHocPhansId",
                table: "GiangVienLopHocPhan",
                column: "LopHocPhansId");

            migrationBuilder.CreateIndex(
                name: "IX_GiangViens_KhoaId",
                table: "GiangViens",
                column: "KhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_GiangViens_TaiKhoanId",
                table: "GiangViens",
                column: "TaiKhoanId",
                unique: true);

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
                name: "IX_PLOs_CTDTId",
                table: "PLOs",
                column: "CTDTId");

            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_KhoaId",
                table: "SinhViens",
                column: "KhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_TaiKhoanId",
                table: "SinhViens",
                column: "TaiKhoanId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CLOCauHoi");

            migrationBuilder.DropTable(
                name: "CLOPLO");

            migrationBuilder.DropTable(
                name: "CTDTHocPhan");

            migrationBuilder.DropTable(
                name: "GiangVienLopHocPhan");

            migrationBuilder.DropTable(
                name: "HocPhanPLO");

            migrationBuilder.DropTable(
                name: "KetQuas");

            migrationBuilder.DropTable(
                name: "LopHocPhanSinhVien");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CLOs");

            migrationBuilder.DropTable(
                name: "GiangViens");

            migrationBuilder.DropTable(
                name: "PLOs");

            migrationBuilder.DropTable(
                name: "CauHois");

            migrationBuilder.DropTable(
                name: "SinhViens");

            migrationBuilder.DropTable(
                name: "CTDTs");

            migrationBuilder.DropTable(
                name: "BaiKiemTras");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Nganhs");

            migrationBuilder.DropTable(
                name: "LopHocPhans");

            migrationBuilder.DropTable(
                name: "HocPhans");

            migrationBuilder.DropTable(
                name: "Khoas");
        }
    }
}
