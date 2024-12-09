using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        
        public DbSet<BaiKiemTra> BaiKiemTras { get; set; } = default!;
        public DbSet<CauHoi> CauHois { get; set; } = default!;
        public DbSet<CLO> CLOs { get; set; } = default!;
        public DbSet<DiemDinhChinh> DiemDinhChinhs { get; set; } = default!;
        public DbSet<GiangVien> GiangViens { get; set; } = default!;
        public DbSet<HocKy> HocKies { get; set; } = default!;
        public DbSet<HocPhan> HocPhans { get; set; } = default!;
        public DbSet<KetQua> KetQuas { get; set; } = default!;
        public DbSet<Khoa> Khoas { get; set; } = default!;
        public DbSet<LopHocPhan> LopHocPhans { get; set; } = default!;
        public DbSet<Nganh> Nganhs { get; set; } = default!;
        public DbSet<PLO> PLOs { get; set; } = default!;
        public DbSet<SinhVien> SinhViens { get; set; } = default!;
        public DbSet<TaiKhoan> TaiKhoans { get; set; } = default!;
        public DbSet<ChucVu> ChucVus { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SinhVien>()
                .HasOne(e => e.TaiKhoan)
                 .WithOne()         
                .HasForeignKey<SinhVien>(s => s.TaiKhoanId) // FK
                .OnDelete(DeleteBehavior.SetNull); 

            modelBuilder.Entity<GiangVien>()
                .HasOne(e => e.TaiKhoan)
                .WithOne()          
                .HasForeignKey<GiangVien>(s => s.TaiKhoanId) // FK
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<CLO>()
                .HasMany(e => e.CauHois)
                .WithMany(e => e.CLOs)
                .UsingEntity(
                    l => l.HasOne(typeof(CauHoi)).WithMany().OnDelete(DeleteBehavior.Cascade),
                    r => r.HasOne(typeof(CLO)).WithMany().OnDelete(DeleteBehavior.ClientCascade));

            modelBuilder.Entity<Khoa>()
                .HasMany(e => e.GiangViens)
                .WithOne(e => e.Khoa)
                .HasForeignKey(e => e.KhoaId)
                .OnDelete(DeleteBehavior.SetNull); // Set KhoaId to null in related GiangViens

            modelBuilder.Entity<Khoa>()
                .Property(k => k.Id)
                .ValueGeneratedOnAdd();  // Cấu hình cho Id sử dụng identity.

            modelBuilder.Entity<Khoa>()
                .HasMany(e => e.Nganhs)
                .WithOne(e => e.Khoa)
                .HasForeignKey(e => e.KhoaId)
                .OnDelete(DeleteBehavior.Cascade); // Set KhoaId to null in related Nganhs

            modelBuilder.Entity<Khoa>()
                .HasMany(e => e.HocPhans)
                .WithOne(e => e.Khoa)
                .HasForeignKey(e => e.KhoaId)
                .OnDelete(DeleteBehavior.Cascade); // Set KhoaId to null in related HocPhan
            
        }
    }
}
