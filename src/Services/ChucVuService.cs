using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Services
{
    public class ChucVuService : IChucVuService
    {
        private readonly ApplicationDBContext _context;
        public ChucVuService(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<ChucVu?> GetIdChucVuByTen(string tenChucVu)
        {
            ChucVu? chucVu = await _context.ChucVus.FirstOrDefaultAsync(x => x.TenChucVu == tenChucVu);
            if (chucVu == null)
            {
                return null;
            }
            return chucVu;
        }

        public async Task<List<string>> GetListChucVu()
        {
            List<string> chucVus = new List<string>();
            chucVus = await _context.ChucVus.Select(x => x.TenChucVu).ToListAsync();
            return chucVus;
        }
    }
}
