using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Data
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options, IUserContext userContext) : DbContext(options)
    {
        private readonly IUserContext _userContext = userContext;

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
        public DbSet<Ctdt> Ctdts { get; set; } = default!;
        public DbSet<UserActivityLog> UserActivityLogs { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Nganh>()
                .HasMany(e => e.HocPhans)
                .WithMany(e => e.Nganhs)
                .UsingEntity<Ctdt>();

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

            modelBuilder.Entity<Nganh>()
                .HasOne(n => n.TaiKhoan)
                .WithMany(t => t.Nganhs)
                .HasForeignKey(n => n.TaiKhoanId) // FK
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

            modelBuilder.Entity<Khoa>()
                .Property(k => k.Id)
                .ValueGeneratedOnAdd();  // Cấu hình cho Id sử dụng identity.

            List<ChucVu> list_chuc_vu = new List<ChucVu>{
                new ChucVu{Id = 1, TenChucVu = "Admin"},
                new ChucVu{Id = 2, TenChucVu = "GiangVien"},
                new ChucVu{Id = 3, TenChucVu = "SinhVien"},
                new ChucVu{Id = 4, TenChucVu = "TruongKhoa"},
                new ChucVu{Id = 5, TenChucVu = "PhongDaoTao"},
                new ChucVu{Id = 6, TenChucVu = "NguoiPhuTrachCTĐT"},
            };
            modelBuilder.Entity<ChucVu>().HasData(list_chuc_vu);
        }

        public override int SaveChanges()
        {
            // Track entities and their states before saving
            var changedEntities = GetChangedEntities();
            
            // Perform the actual save
            var result = base.SaveChanges();
            
            // Create and save audit logs after changes are committed
            CreateAndSaveAuditLogs(changedEntities);
            
            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Track entities and their states before saving
            var changedEntities = GetChangedEntities();
            
            // Perform the actual save
            var result = await base.SaveChangesAsync(cancellationToken);
            
            // Create and save audit logs after changes are committed
            await CreateAndSaveAuditLogsAsync(changedEntities, cancellationToken);
            
            return result;
        }

        // Track entity changes before saving
        private Dictionary<EntityEntry, EntityState> GetChangedEntities()
        {
            ChangeTracker.DetectChanges();
            
            var changedEntities = new Dictionary<EntityEntry, EntityState>();
            
            foreach (var entry in ChangeTracker.Entries())
            {
                // Skip UserActivityLog entities and unchanged/detached entities
                if (entry.Entity is UserActivityLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                
                // Store the original state
                changedEntities[entry] = entry.State;
            }
            
            return changedEntities;
        }

        // Create and save audit logs synchronously
        private void CreateAndSaveAuditLogs(Dictionary<EntityEntry, EntityState> changedEntities)
        {
            if (changedEntities.Count == 0)
                return;
            
            try
            {
                var userActivityLogs = CreateUserActivityLogs(changedEntities);
                
                // Add the logs to the context
                UserActivityLogs.AddRange(userActivityLogs);
                
                // Call SaveChanges without triggering this again
                using var transaction = Database.BeginTransaction();
                SaveChangesWithoutAuditing();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                // Log the error but don't crash the application
                Console.Error.WriteLine($"Error saving audit logs: {ex.Message}");
            }
        }

        // Create and save audit logs asynchronously
        private async Task CreateAndSaveAuditLogsAsync(Dictionary<EntityEntry, EntityState> changedEntities, CancellationToken cancellationToken = default)
        {
            if (changedEntities.Count == 0)
                return;
            
            try
            {
                var userActivityLogs = CreateUserActivityLogs(changedEntities);
                
                // Add the logs to the context
                await UserActivityLogs.AddRangeAsync(userActivityLogs, cancellationToken);
                
                // Call SaveChangesAsync without triggering this again
                using var transaction = await Database.BeginTransactionAsync(cancellationToken);
                await SaveChangesWithoutAuditingAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                // Log the error but don't crash the application
                Console.Error.WriteLine($"Error saving audit logs: {ex.Message}");
            }
        }

        // Create user activity logs from changed entities
        private List<UserActivityLog> CreateUserActivityLogs(Dictionary<EntityEntry, EntityState> changedEntities)
        {
            var userActivityLogs = new List<UserActivityLog>();
            
            // Define JSON serialization settings
            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            
            foreach (var entityEntry in changedEntities)
            {
                var entry = entityEntry.Key;
                var state = entityEntry.Value;
                
                var entityBefore = new Dictionary<string, object>();
                var entityAfter = new Dictionary<string, object>();
                
                // Process properties based on the state that was tracked before saving
                foreach (var property in entry.Properties)
                {
                    var propertyName = property.Metadata.Name;
                    
                    switch (state)
                    {
                        case EntityState.Added:
                            // For added entities, we now have real ID values after the save
                            entityAfter[propertyName] = property.CurrentValue ?? DBNull.Value;
                            break;
                        
                        case EntityState.Deleted:
                            entityBefore[propertyName] = property.OriginalValue ?? DBNull.Value;
                            break;
                        
                        case EntityState.Modified:
                            entityBefore[propertyName] = property.OriginalValue ?? DBNull.Value;
                            entityAfter[propertyName] = property.CurrentValue ?? DBNull.Value;
                            break;
                    }
                }
                
                // Create the audit log entry
                var userActivityLog = new UserActivityLog
                {
                    UserId = _userContext.UserId,
                    UserName = _userContext.UserName,
                    UserRole = _userContext.UserRole,
                    Action = entry.Metadata.GetTableName() + "." + state.ToString(),
                    EntityBefore = JsonConvert.SerializeObject(entityBefore, jsonSettings),
                    EntityAfter = JsonConvert.SerializeObject(entityAfter, jsonSettings),
                    Timestamp = DateTime.UtcNow,
                    IpAddress = _userContext.UserIpAddress
                };
                
                userActivityLogs.Add(userActivityLog);
            }
            
            return userActivityLogs;
        }

        // Methods to avoid recursive auditing when saving audit logs
        private int SaveChangesWithoutAuditing()
        {
            return base.SaveChanges();
        }

        private Task<int> SaveChangesWithoutAuditingAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
