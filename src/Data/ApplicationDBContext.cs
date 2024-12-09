using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Data
{
    public class ApplicationDBContext : IdentityDbContext<TaiKhoan>
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            // modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
            // modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SinhVien>()
                .HasOne(e => e.TaiKhoan)
                 .WithOne()         
                .HasForeignKey<SinhVien>(s => s.TaiKhoanId) // FK
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<GiangVien>()
                .HasOne(e => e.TaiKhoan)
                .WithOne()          
                .HasForeignKey<GiangVien>(s => s.TaiKhoanId) // FK
                .OnDelete(DeleteBehavior.Cascade);

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
                .OnDelete(DeleteBehavior.SetNull); // Set KhoaId to null in related Nganhs

            modelBuilder.Entity<Khoa>()
                .HasMany(e => e.HocPhans)
                .WithOne(e => e.Khoa)
                .HasForeignKey(e => e.KhoaId)
                .OnDelete(DeleteBehavior.SetNull); // Set KhoaId to null in related HocPhans


            modelBuilder.Entity<Khoa>()
                .HasOne(e => e.TruongKhoa)
                .WithOne()
                .HasForeignKey<Khoa>(e => e.TruongKhoaId)
                .OnDelete(DeleteBehavior.SetNull); // Set TruongKhoaId to null in related Khoa

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole {Name = "Admin", NormalizedName = "ADMIN"},
                new IdentityRole {Name = "GiangVien", NormalizedName = "GIANGVIEN"},
                new IdentityRole {Name = "SinhVien", NormalizedName = "SINHVIEN"},
                new IdentityRole {Name = "PhongDaoTao", NormalizedName = "PHONGDAOTAO"},
                new IdentityRole {Name = "TruongKhoa", NormalizedName = "TRUONGKHOA"},
                new IdentityRole {Name = "TruongBoMon", NormalizedName = "TRUONGBOMON"}
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            List<Khoa> khoas = new List<Khoa>
            {
                new Khoa { Id = 1, Ten = "Cơ khí", MaKhoa = "101" },
                new Khoa { Id = 2, Ten = "Công nghệ thông tin", MaKhoa = "102" },
                new Khoa { Id = 3, Ten = "Cơ khí giao thông", MaKhoa = "103" },
                new Khoa { Id = 4, Ten = "Công nghệ nhiệt điện lạnh", MaKhoa = "104"},
                new Khoa { Id = 5, Ten = "Điện", MaKhoa = "105" },
                new Khoa { Id = 6, Ten = "Điện tử viễn thông", MaKhoa = "106" },
                new Khoa { Id = 7, Ten = "Hóa", MaKhoa = "107"},
                new Khoa { Id = 8, Ten = "Xây dựng cầu đường", MaKhoa = "108" },
                new Khoa { Id = 9, Ten = "Xây dựng dân dụng và công nghiệp", MaKhoa = "109" },
                new Khoa { Id = 10, Ten = "Xây dựng công trình thủy", MaKhoa = "110" },
                new Khoa { Id = 11, Ten = "Môi trường", MaKhoa = "111" },
                new Khoa { Id = 12, Ten = "Quản lý dự án", MaKhoa = "112" },
                new Khoa { Id = 13, Ten = "Kiến trúc", MaKhoa = "113" },
                new Khoa { Id = 14, Ten = "Khoa học công nghệ tiên tiến", MaKhoa = "114" }
            };

            modelBuilder.Entity<Khoa>().HasData(khoas);
            
        }
    }
}
