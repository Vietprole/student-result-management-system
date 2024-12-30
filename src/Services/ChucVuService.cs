using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.ChucVu;
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
        public async Task<ChucVu?> GetChucVuById(int chucVuId)
        {
            ChucVu? chucVu = await _context.ChucVus.FirstOrDefaultAsync(x => x.Id == chucVuId);
            if (chucVu == null)
            {
                return null;
            }
            return chucVu;
        }

        public async Task<List<ChucVu>> GetListChucVu()
        {
            var chucVus = await _context.ChucVus.ToListAsync();
            return chucVus;
        }
    }
}
