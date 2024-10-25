using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        
        public DbSet<BaiKiemTra> BaiKiemTras { get; set; }
        public DbSet<CauHoi> CauHois { get; set; }
        public DbSet<CLO> CLOs { get; set; }
        public DbSet<CTDT> CTDTs {get; set;}
        public DbSet<GiangVien> GiangViens { get; set; }
        public DbSet<HocKy> HocKies { get; set; }
        public DbSet<HocPhan> HocPhans { get; set; }
        public DbSet<KetQua> KetQuas { get; set; }
        public DbSet<Khoa> Khoas { get; set; }
        public DbSet<LopHocPhan> LopHocPhans { get; set; }
        public DbSet<Nganh> Nganhs { get; set; }
        public DbSet<PLO> PLOs { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }
    }
}
