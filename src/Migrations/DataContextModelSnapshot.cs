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

            modelBuilder.Entity("CTDTHocPhan", b =>
                {
                    b.Property<int>("CTDTsId")
                        .HasColumnType("int");

                    b.Property<int>("HocPhansId")
                        .HasColumnType("int");

                    b.HasKey("CTDTsId", "HocPhansId");

                    b.HasIndex("HocPhansId");

                    b.ToTable("CTDTHocPhan");
                });

            modelBuilder.Entity("GiangVienLopHocPhan", b =>
                {
                    b.Property<int>("GiangViensId")
                        .HasColumnType("int");

                    b.Property<int>("LopHocPhansId")
                        .HasColumnType("int");

                    b.HasKey("GiangViensId", "LopHocPhansId");

                    b.HasIndex("LopHocPhansId");

                    b.ToTable("GiangVienLopHocPhan");
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

                    b.Property<string>("Loai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LopHocPhanId")
                        .HasColumnType("int");

                    b.Property<float>("TrongSo")
                        .HasColumnType("real");

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

                    b.ToTable("CTDTs");
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

                    b.ToTable("CauHois");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.GiangVien", b =>
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

                    b.ToTable("GiangViens");
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

                    b.Property<float>("Diem")
                        .HasColumnType("real");

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

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Khoas");
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

                    b.ToTable("LopHocPhans");
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

                    b.ToTable("Nganhs");
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

                    b.ToTable("PLOs");
                });

            modelBuilder.Entity("Student_Result_Management_System.Models.SinhVien", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SinhViens");
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

                    b.Navigation("Khoa");
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
