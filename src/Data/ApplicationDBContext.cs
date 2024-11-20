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
            try
            {
                // Apply any pending migrations
                Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public DbSet<BaiKiemTra> BaiKiemTras { get; set; }
        public DbSet<CauHoi> CauHois { get; set; }
        public DbSet<CLO> CLOs { get; set; }
        public DbSet<CTDT> CTDTs {get; set;}
        public DbSet<GiangVien> GiangViens { get; set; }
        public DbSet<HocPhan> HocPhans { get; set; }
        public DbSet<KetQua> KetQuas { get; set; }
        public DbSet<Khoa> Khoas { get; set; }
        public DbSet<LopHocPhan> LopHocPhans { get; set; }
        public DbSet<Nganh> Nganhs { get; set; }
        public DbSet<PLO> PLOs { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                .HasMany(e => e.Nganhs)
                .WithOne(e => e.Khoa)
                .HasForeignKey(e => e.KhoaId)
                .OnDelete(DeleteBehavior.SetNull); // Set KhoaId to null in related Nganhs

            modelBuilder.Entity<Khoa>()
                .HasMany(e => e.HocPhans)
                .WithOne(e => e.Khoa)
                .HasForeignKey(e => e.KhoaId)
                .OnDelete(DeleteBehavior.SetNull); // Set KhoaId to null in related HocPhans

            modelBuilder.Entity<Nganh>()
                .HasMany(e => e.CTDTs)
                .WithOne(e => e.Nganh)
                .HasForeignKey(e => e.NganhId)
                .OnDelete(DeleteBehavior.SetNull); // Set NganhId to null in related CTDTs
        }
    }
}
