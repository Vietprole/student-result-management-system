﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Student_Result_Management_System.Data;

#nullable disable

namespace Student_Result_Management_System.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CLOCauHoi", b =>
                {
                    b.Property<int>("CLOsId")
                        .HasColumnType("int");

                    b.Property<int>("CauHoisId")
                        .HasColumnType("int");

                    b.HasKey("CLOsId", "CauHoisId");

                    b.HasIndex("CauHoisId");

                    b.ToTable("CLOCauHoi");
                });

            modelBuilder.Entity("CLOPLO", b =>
                {
                    b.Property<int>("CLOsId")
                        .HasColumnType("int");

                    b.Property<int>("PLOsId")
                        .HasColumnType("int");

                    b.HasKey("CLOsId", "PLOsId");

                    b.HasIndex("PLOsId");

                    b.ToTable("CLOPLO");
                });

            modelBuilder.Entity("HocPhanNganh", b =>
                {
                    b.Property<int>("HocPhansId")
                        .HasColumnType("int");

                    b.Property<int>("NganhsId")
                        .HasColumnType("int");

                    b.HasKey("HocPhansId", "NganhsId");

                    b.HasIndex("NganhsId");

                    b.ToTable("HocPhanNganh");
                });

            modelBuilder.Entity("HocPhanPLO", b =>
                {
                    b.Property<int>("HocPhansId")
                        .HasColumnType("int");

                    b.Property<int>("PLOsId")
                        .HasColumnType("int");

                    b.HasKey("HocPhansId", "PLOsId");

                    b.HasIndex("PLOsId");

                    b.ToTable("HocPhanPLO");
                });

            modelBuilder.Entity("LopHocPhanSinhVien", b =>
                {
                    b.Property<int>("LopHocPhansId")
                        .HasColumnType("int");

                    b.Property<int>("SinhViensId")
                        .HasColumnType("int");

                    b.HasKey("LopHocPhansId", "SinhViensId");

                    b.HasIndex("SinhViensId");

                    b.ToTable("LopHocPhanSinhVien");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.BaiKiemTra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("HanDinhChinh")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("HanNhapDiem")
                        .HasColumnType("datetime2");

                    b.Property<string>("Loai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LopHocPhanId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayMoNhapDiem")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayXacNhan")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("TrongSo")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<decimal?>("TrongSoDeXuat")
                        .HasColumnType("decimal(5, 2)");

                    b.HasKey("Id");

                    b.HasIndex("LopHocPhanId");

                    b.ToTable("BaiKiemTras");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.CLO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LopHocPhanId")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LopHocPhanId");

                    b.ToTable("CLOs");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.CauHoi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BaiKiemTraId")
                        .HasColumnType("int");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ThangDiem")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<decimal>("TrongSo")
                        .HasColumnType("decimal(5, 2)");

                    b.HasKey("Id");

                    b.HasIndex("BaiKiemTraId");

                    b.ToTable("CauHois");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.ChucVu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TenChucVu")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("ChucVus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TenChucVu = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            TenChucVu = "GiangVien"
                        },
                        new
                        {
                            Id = 3,
                            TenChucVu = "SinhVien"
                        },
                        new
                        {
                            Id = 4,
                            TenChucVu = "TruongKhoa"
                        },
                        new
                        {
                            Id = 5,
                            TenChucVu = "PhongDaoTao"
                        },
                        new
                        {
                            Id = 6,
                            TenChucVu = "TruongBoMon"
                        });
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.DiemDinhChinh", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("DiemMoi")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<bool>("DuocDuyet")
                        .HasColumnType("bit");

                    b.Property<int>("KetQuaId")
                        .HasColumnType("int");

                    b.Property<int>("NguoiDuyetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ThoiGian")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("KetQuaId");

                    b.ToTable("DiemDinhChinhs");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.GiangVien", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("KhoaId")
                        .HasColumnType("int");

                    b.Property<string>("MaGiangVien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TaiKhoanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KhoaId");

                    b.HasIndex("TaiKhoanId")
                        .IsUnique()
                        .HasFilter("[TaiKhoanId] IS NOT NULL");

                    b.ToTable("GiangViens");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.HocKy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MaHocKy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NamHoc")
                        .HasColumnType("int");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HocKies");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.HocPhan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("KhoaId")
                        .HasColumnType("int");

                    b.Property<bool>("LaCotLoi")
                        .HasColumnType("bit");

                    b.Property<string>("MaHocPhan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SoTinChi")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KhoaId");

                    b.ToTable("HocPhans");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.KetQua", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CauHoiId")
                        .HasColumnType("int");

                    b.Property<bool>("DaCongBo")
                        .HasColumnType("bit");

                    b.Property<bool>("DaXacNhan")
                        .HasColumnType("bit");

                    b.Property<decimal?>("DiemChinhThuc")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<decimal>("DiemTam")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<int>("SinhVienId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CauHoiId");

                    b.HasIndex("SinhVienId");

                    b.ToTable("KetQuas");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.Khoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MaKhoa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TruongKhoaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TruongKhoaId");

                    b.ToTable("Khoas");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.LopHocPhan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("GiangVienId")
                        .HasColumnType("int");

                    b.Property<DateTime>("HanDeXuatCongThucDiem")
                        .HasColumnType("datetime2");

                    b.Property<int>("HocKyId")
                        .HasColumnType("int");

                    b.Property<int>("HocPhanId")
                        .HasColumnType("int");

                    b.Property<string>("MaLopHocPhan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayChapNhanCongThucDiem")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NguoiChapNhanCongThucDiemId")
                        .HasColumnType("int");

                    b.Property<int?>("NguoiDeXuatCongThucDiemId")
                        .HasColumnType("int");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GiangVienId");

                    b.HasIndex("HocKyId");

                    b.HasIndex("HocPhanId");

                    b.ToTable("LopHocPhans");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.Nganh", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("KhoaId")
                        .HasColumnType("int");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KhoaId");

                    b.ToTable("Nganhs");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.PLO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NganhId")
                        .HasColumnType("int");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NganhId");

                    b.ToTable("PLOs");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.SinhVien", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("KhoaId")
                        .HasColumnType("int");

                    b.Property<int>("NamNhapHoc")
                        .HasColumnType("int");

                    b.Property<int?>("TaiKhoanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KhoaId");

                    b.HasIndex("TaiKhoanId")
                        .IsUnique()
                        .HasFilter("[TaiKhoanId] IS NOT NULL");

                    b.ToTable("SinhViens");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.TaiKhoan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChucVuId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ChucVuId");

                    b.ToTable("TaiKhoans");
                });

            modelBuilder.Entity("CLOCauHoi", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.CLO", null)
                        .WithMany()
                        .HasForeignKey("CLOsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Student_Result_Management_System.Models.CauHoi", null)
                        .WithMany()
                        .HasForeignKey("CauHoisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CLOPLO", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.CLO", null)
                        .WithMany()
                        .HasForeignKey("CLOsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Student_Result_Management_System.Models.PLO", null)
                        .WithMany()
                        .HasForeignKey("PLOsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HocPhanNganh", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.HocPhan", null)
                        .WithMany()
                        .HasForeignKey("HocPhansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Student_Result_Management_System.Models.Nganh", null)
                        .WithMany()
                        .HasForeignKey("NganhsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HocPhanPLO", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.HocPhan", null)
                        .WithMany()
                        .HasForeignKey("HocPhansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Student_Result_Management_System.Models.PLO", null)
                        .WithMany()
                        .HasForeignKey("PLOsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LopHocPhanSinhVien", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.LopHocPhan", null)
                        .WithMany()
                        .HasForeignKey("LopHocPhansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Student_Result_Management_System.Models.SinhVien", null)
                        .WithMany()
                        .HasForeignKey("SinhViensId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.BaiKiemTra", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.LopHocPhan", "LopHocPhan")
                        .WithMany()
                        .HasForeignKey("LopHocPhanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("LopHocPhan");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.CLO", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.LopHocPhan", "LopHocPhan")
                        .WithMany("CLOs")
                        .HasForeignKey("LopHocPhanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("LopHocPhan");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.CauHoi", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.BaiKiemTra", "BaiKiemTra")
                        .WithMany("CauHois")
                        .HasForeignKey("BaiKiemTraId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BaiKiemTra");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.DiemDinhChinh", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.KetQua", "KetQua")
                        .WithMany()
                        .HasForeignKey("KetQuaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("KetQua");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.GiangVien", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.Khoa", "Khoa")
                        .WithMany("GiangViens")
                        .HasForeignKey("KhoaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Student_Result_Management_System.Models.TaiKhoan", "TaiKhoan")
                        .WithOne()
                        .HasForeignKey("Student_Result_Management_System.Models.GiangVien", "TaiKhoanId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Khoa");

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.HocPhan", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.Khoa", "Khoa")
                        .WithMany("HocPhans")
                        .HasForeignKey("KhoaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Khoa");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.KetQua", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.CauHoi", "CauHoi")
                        .WithMany()
                        .HasForeignKey("CauHoiId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Student_Result_Management_System.Models.SinhVien", "SinhVien")
                        .WithMany()
                        .HasForeignKey("SinhVienId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CauHoi");

                    b.Navigation("SinhVien");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.Khoa", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.TaiKhoan", "TruongKhoa")
                        .WithMany()
                        .HasForeignKey("TruongKhoaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("TruongKhoa");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.LopHocPhan", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.GiangVien", "GiangVien")
                        .WithMany("LopHocPhans")
                        .HasForeignKey("GiangVienId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Student_Result_Management_System.Models.HocKy", "HocKy")
                        .WithMany()
                        .HasForeignKey("HocKyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Student_Result_Management_System.Models.HocPhan", "HocPhan")
                        .WithMany("LopHocPhans")
                        .HasForeignKey("HocPhanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("GiangVien");

                    b.Navigation("HocKy");

                    b.Navigation("HocPhan");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.Nganh", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.Khoa", "Khoa")
                        .WithMany("Nganhs")
                        .HasForeignKey("KhoaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Khoa");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.PLO", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.Nganh", "Nganh")
                        .WithMany()
                        .HasForeignKey("NganhId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Nganh");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.SinhVien", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.Khoa", "Khoa")
                        .WithMany("SinhViens")
                        .HasForeignKey("KhoaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Student_Result_Management_System.Models.TaiKhoan", "TaiKhoan")
                        .WithOne()
                        .HasForeignKey("Student_Result_Management_System.Models.SinhVien", "TaiKhoanId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Khoa");

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.TaiKhoan", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.ChucVu", "ChucVu")
                        .WithMany()
                        .HasForeignKey("ChucVuId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ChucVu");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.BaiKiemTra", b =>
                {
                    b.Navigation("CauHois");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.GiangVien", b =>
                {
                    b.Navigation("LopHocPhans");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.HocPhan", b =>
                {
                    b.Navigation("LopHocPhans");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.Khoa", b =>
                {
                    b.Navigation("GiangViens");

                    b.Navigation("HocPhans");

                    b.Navigation("Nganhs");

                    b.Navigation("SinhViens");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.LopHocPhan", b =>
                {
                    b.Navigation("CLOs");
                });
#pragma warning restore 612, 618
        }
    }
}
