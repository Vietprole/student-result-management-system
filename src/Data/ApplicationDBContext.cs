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
        // public DbSet<Ctdt> Ctdts { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Get all entity types
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Get all foreign keys for this entity
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    // Set DeleteBehavior.Restrict as default
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }

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

            // modelBuilder.Entity<GiangVien>()
            //     .HasMany(e => e.LopHocPhans)
            //     .WithOne(e => e.GiangVien)
            //     .HasForeignKey(e => e.GiangVienId)
            //     .OnDelete(DeleteBehavior.SetNull); //Set GiangVienId in LopHocPhan to null when GiangVien is deleted

            // modelBuilder.Entity<Khoa>()
            //     .HasMany(e => e.Nganhs)
            //     .WithOne(e => e.Khoa)
            //     .HasForeignKey(e => e.KhoaId)
            //     .OnDelete(DeleteBehavior.Restrict); // Prevent Khoa from being deleted if there are Nganhs associated with it

            // modelBuilder.Entity<HocPhan>()
            //     .HasMany(e => e.Nganhs)
            //     .WithMany(e => e.HocPhans)
            //     .UsingEntity(
            //         l => l.HasOne(typeof(Nganh)).WithMany().OnDelete(DeleteBehavior.Cascade),   // When Nganh is deleted, delete join entries
            //         r => r.HasOne(typeof(HocPhan)).WithMany().OnDelete(DeleteBehavior.ClientCascade)); // When HocPhan is deleted, delete join entries client-side

            // modelBuilder.Entity<LopHocPhan>()
            //     .HasMany(e => e.SinhViens)
            //     .WithMany(e => e.LopHocPhans)
            //     .UsingEntity(
            //         l => l.HasOne(typeof(SinhVien)).WithMany().OnDelete(DeleteBehavior.Cascade),   // When SinhVien is deleted, delete join entries
            //         r => r.HasOne(typeof(LopHocPhan)).WithMany().OnDelete(DeleteBehavior.ClientCascade)); // When LopHocPhan is deleted, delete join entries client-side

            modelBuilder.Entity<CLO>()
                .HasMany(e => e.CauHois)
                .WithMany(e => e.CLOs)
                .UsingEntity(
                    l => l.HasOne(typeof(CauHoi)).WithMany().OnDelete(DeleteBehavior.Cascade),   // When CauHoi is deleted, delete join entries
                    r => r.HasOne(typeof(CLO)).WithMany().OnDelete(DeleteBehavior.Cascade)); // When CLO is deleted, delete join entries 

            modelBuilder.Entity<PLO>()
                .HasMany(e => e.CLOs)
                .WithMany(e => e.PLOs)
                .UsingEntity(
                    l => l.HasOne(typeof(CLO)).WithMany().OnDelete(DeleteBehavior.Cascade),   // When CLO is deleted, delete join entries
                    r => r.HasOne(typeof(PLO)).WithMany().OnDelete(DeleteBehavior.Cascade)); // When PLO is deleted, delete join entries 

            modelBuilder.Entity<HocPhan>()
                .HasMany(e => e.PLOs)
                .WithMany(e => e.HocPhans)
                .UsingEntity(
                    l => l.HasOne(typeof(PLO)).WithMany().OnDelete(DeleteBehavior.Cascade),   // When CLO is deleted, delete join entries
                    r => r.HasOne(typeof(HocPhan)).WithMany().OnDelete(DeleteBehavior.Cascade)); // When HocPhan is deleted, delete join entries 

            modelBuilder.Entity<SinhVien>()
                .HasMany(e => e.LopHocPhans)
                .WithMany(e => e.SinhViens)
                .UsingEntity(
                    l => l.HasOne(typeof(LopHocPhan)).WithMany().OnDelete(DeleteBehavior.Cascade),   // When LopHocPhan is deleted, delete join entries
                    r => r.HasOne(typeof(SinhVien)).WithMany().OnDelete(DeleteBehavior.Cascade)); // When SinhVien is deleted, delete join entries 

            // modelBuilder.Entity<Nganh>()
            //     .HasMany(e => e.HocPhans)
            //     .WithMany(e => e.Nganhs)
            //     .UsingEntity(
            //         l => l.HasOne(typeof(HocPhan)).WithMany().OnDelete(DeleteBehavior.Cascade),   // When HocPhan is deleted, delete join entries
            //         r => r.HasOne(typeof(Nganh)).WithMany().OnDelete(DeleteBehavior.Cascade)); // When Nganh is deleted, delete join entries 

            // modelBuilder.Entity<Khoa>()
            //     .HasMany(e => e.GiangViens)
            //     .WithOne(e => e.Khoa)
            //     .HasForeignKey(e => e.KhoaId)
            //     .OnDelete(DeleteBehavior.SetNull); // Set KhoaId in GiangVien to null when Khoa is deleted

            modelBuilder.Entity<Khoa>()
                .Property(k => k.Id)
                .ValueGeneratedOnAdd();  // Cấu hình cho Id sử dụng identity.

            // modelBuilder.Entity<Khoa>()
            //     .HasMany(e => e.Nganhs)
            //     .WithOne(e => e.Khoa)
            //     .HasForeignKey(e => e.KhoaId)
            //     .OnDelete(DeleteBehavior.Cascade);

            // modelBuilder.Entity<Khoa>()
            //     .HasMany(e => e.HocPhans)
            //     .WithOne(e => e.Khoa)
            //     .HasForeignKey(e => e.KhoaId)
            //     .OnDelete(DeleteBehavior.Cascade);

            List<ChucVu> list_chuc_vu = new List<ChucVu>{
                new ChucVu{Id = 1, TenChucVu = "Admin"},
                new ChucVu{Id = 2, TenChucVu = "GiangVien"},
                new ChucVu{Id = 3, TenChucVu = "SinhVien"},
                new ChucVu{Id = 4, TenChucVu = "TruongKhoa"},
                new ChucVu{Id = 5, TenChucVu = "PhongDaoTao"},
                new ChucVu{Id = 6, TenChucVu = "TruongBoMon"},
            };
            modelBuilder.Entity<ChucVu>().HasData(list_chuc_vu);
        }
    }
}
