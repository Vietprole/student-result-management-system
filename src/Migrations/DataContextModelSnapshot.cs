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
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
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

                    b.ToTable("CLOCauHoi", (string)null);
                });

            modelBuilder.Entity("CLOPLO", b =>
                {
                    b.Property<int>("CLOsId")
                        .HasColumnType("int");

                    b.Property<int>("PLOsId")
                        .HasColumnType("int");

                    b.HasKey("CLOsId", "PLOsId");

                    b.HasIndex("PLOsId");

                    b.ToTable("CLOPLO", (string)null);
                });

            modelBuilder.Entity("CTDTHocPhan", b =>
                {
                    b.Property<int>("CTDTsId")
                        .HasColumnType("int");

                    b.Property<int>("HocPhansId")
                        .HasColumnType("int");

                    b.HasKey("CTDTsId", "HocPhansId");

                    b.HasIndex("HocPhansId");

                    b.ToTable("CTDTHocPhan", (string)null);
                });

            modelBuilder.Entity("GiangVienLopHocPhan", b =>
                {
                    b.Property<int>("GiangViensId")
                        .HasColumnType("int");

                    b.Property<int>("LopHocPhansId")
                        .HasColumnType("int");

                    b.HasKey("GiangViensId", "LopHocPhansId");

                    b.HasIndex("LopHocPhansId");

                    b.ToTable("GiangVienLopHocPhan", (string)null);
                });

            modelBuilder.Entity("HocPhanPLO", b =>
                {
                    b.Property<int>("HocPhansId")
                        .HasColumnType("int");

                    b.Property<int>("PLOsId")
                        .HasColumnType("int");

                    b.HasKey("HocPhansId", "PLOsId");

                    b.HasIndex("PLOsId");

                    b.ToTable("HocPhanPLO", (string)null);
                });

            modelBuilder.Entity("LopHocPhanSinhVien", b =>
                {
                    b.Property<int>("LopHocPhansId")
                        .HasColumnType("int");

                    b.Property<int>("SinhViensId")
                        .HasColumnType("int");

                    b.HasKey("LopHocPhansId", "SinhViensId");

                    b.HasIndex("SinhViensId");

                    b.ToTable("LopHocPhanSinhVien", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "f1e36b8b-b646-447d-bf82-460672b7ae98",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "26bf66c8-0edc-417c-a5ef-6c553019fc14",
                            Name = "GiangVien",
                            NormalizedName = "GIANGVIEN"
                        },
                        new
                        {
                            Id = "dcd22d3d-471b-4c95-a0e7-fc9bedf5a3f8",
                            Name = "SinhVien",
                            NormalizedName = "SINHVIEN"
                        },
                        new
                        {
                            Id = "dc608489-0e5e-4945-bca6-688d6cf06400",
                            Name = "PhongDaoTao",
                            NormalizedName = "PHONGDAOTAO"
                        },
                        new
                        {
                            Id = "8e8da6b3-07df-4fd5-b4f7-82f0635a9cb0",
                            Name = "TruongKhoa",
                            NormalizedName = "TRUONGKHOA"
                        },
                        new
                        {
                            Id = "e820b120-4f48-4c64-881b-96cc138400d9",
                            Name = "TruongBoMon",
                            NormalizedName = "TRUONGBOMON"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.BaiKiemTra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Loai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LopHocPhanId")
                        .HasColumnType("int");

                    b.Property<float>("TrongSo")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("LopHocPhanId");

                    b.ToTable("BaiKiemTras", (string)null);
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

                    b.ToTable("CLOs", (string)null);
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.CTDT", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("KhoaId")
                        .HasColumnType("int");

                    b.Property<int?>("NganhId")
                        .HasColumnType("int");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KhoaId");

                    b.HasIndex("NganhId");

                    b.ToTable("CTDTs", (string)null);
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

                    b.Property<float>("TrongSo")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("BaiKiemTraId");

                    b.ToTable("CauHois", (string)null);
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.GiangVien", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("KhoaId")
                        .HasColumnType("int");

                    b.Property<string>("TaiKhoanId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("KhoaId");

                    b.HasIndex("TaiKhoanId")
                        .IsUnique();

                    b.ToTable("GiangViens", (string)null);
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.HocPhan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("KhoaId")
                        .HasColumnType("int");

                    b.Property<bool>("LaCotLoi")
                        .HasColumnType("bit");

                    b.Property<float>("SoTinChi")
                        .HasColumnType("real");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KhoaId");

                    b.ToTable("HocPhans", (string)null);
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.KetQua", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CauHoiId")
                        .HasColumnType("int");

                    b.Property<float>("Diem")
                        .HasColumnType("real");

                    b.Property<int>("SinhVienId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CauHoiId");

                    b.HasIndex("SinhVienId");

                    b.ToTable("KetQuas", (string)null);
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

                    b.Property<string>("VietTat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Khoas", (string)null);
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.LopHocPhan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("HocPhanId")
                        .HasColumnType("int");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HocPhanId");

                    b.ToTable("LopHocPhans", (string)null);
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.Nganh", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("KhoaId")
                        .HasColumnType("int");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KhoaId");

                    b.ToTable("Nganhs", (string)null);
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.PLO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CTDTId")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CTDTId");

                    b.ToTable("PLOs", (string)null);
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.SinhVien", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("KhoaId")
                        .HasColumnType("int");

                    b.Property<int>("NamBatDau")
                        .HasColumnType("int");

                    b.Property<string>("TaiKhoanId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("KhoaId");

                    b.HasIndex("TaiKhoanId")
                        .IsUnique();

                    b.ToTable("SinhViens", (string)null);
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.TaiKhoan", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("HovaTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("CLOCauHoi", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.CLO", null)
                        .WithMany()
                        .HasForeignKey("CLOsId")
                        .OnDelete(DeleteBehavior.ClientCascade)
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

            modelBuilder.Entity("CTDTHocPhan", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.CTDT", null)
                        .WithMany()
                        .HasForeignKey("CTDTsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Student_Result_Management_System.Models.HocPhan", null)
                        .WithMany()
                        .HasForeignKey("HocPhansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GiangVienLopHocPhan", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.GiangVien", null)
                        .WithMany()
                        .HasForeignKey("GiangViensId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Student_Result_Management_System.Models.LopHocPhan", null)
                        .WithMany()
                        .HasForeignKey("LopHocPhansId")
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.TaiKhoan", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.TaiKhoan", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Student_Result_Management_System.Models.TaiKhoan", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.TaiKhoan", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.BaiKiemTra", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.LopHocPhan", "LopHocPhan")
                        .WithMany()
                        .HasForeignKey("LopHocPhanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LopHocPhan");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.CLO", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.LopHocPhan", null)
                        .WithMany("CLOs")
                        .HasForeignKey("LopHocPhanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.CTDT", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.Khoa", null)
                        .WithMany("CTDTs")
                        .HasForeignKey("KhoaId");

                    b.HasOne("Student_Result_Management_System.Models.Nganh", "Nganh")
                        .WithMany("CTDTs")
                        .HasForeignKey("NganhId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Nganh");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.CauHoi", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.BaiKiemTra", "BaiKiemTra")
                        .WithMany()
                        .HasForeignKey("BaiKiemTraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BaiKiemTra");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.GiangVien", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.Khoa", "Khoa")
                        .WithMany("GiangViens")
                        .HasForeignKey("KhoaId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Student_Result_Management_System.Models.TaiKhoan", "TaiKhoan")
                        .WithOne()
                        .HasForeignKey("Student_Result_Management_System.Models.GiangVien", "TaiKhoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Khoa");

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.HocPhan", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.Khoa", "Khoa")
                        .WithMany("HocPhans")
                        .HasForeignKey("KhoaId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Khoa");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.KetQua", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.CauHoi", "CauHoi")
                        .WithMany()
                        .HasForeignKey("CauHoiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Student_Result_Management_System.Models.SinhVien", "SinhVien")
                        .WithMany()
                        .HasForeignKey("SinhVienId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CauHoi");

                    b.Navigation("SinhVien");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.LopHocPhan", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.HocPhan", "HocPhan")
                        .WithMany("LopHocPhans")
                        .HasForeignKey("HocPhanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HocPhan");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.Nganh", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.Khoa", "Khoa")
                        .WithMany("Nganhs")
                        .HasForeignKey("KhoaId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Khoa");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.PLO", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.CTDT", "CTDT")
                        .WithMany("PLOs")
                        .HasForeignKey("CTDTId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CTDT");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.SinhVien", b =>
                {
                    b.HasOne("Student_Result_Management_System.Models.Khoa", "Khoa")
                        .WithMany("SinhViens")
                        .HasForeignKey("KhoaId");

                    b.HasOne("Student_Result_Management_System.Models.TaiKhoan", "TaiKhoan")
                        .WithOne()
                        .HasForeignKey("Student_Result_Management_System.Models.SinhVien", "TaiKhoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Khoa");

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.CTDT", b =>
                {
                    b.Navigation("PLOs");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.HocPhan", b =>
                {
                    b.Navigation("LopHocPhans");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.Khoa", b =>
                {
                    b.Navigation("CTDTs");

                    b.Navigation("GiangViens");

                    b.Navigation("HocPhans");

                    b.Navigation("Nganhs");

                    b.Navigation("SinhViens");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.LopHocPhan", b =>
                {
                    b.Navigation("CLOs");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.Nganh", b =>
                {
                    b.Navigation("CTDTs");
                });
#pragma warning restore 612, 618
        }
    }
}
