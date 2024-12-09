using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.BaiKiemTra;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;

namespace Student_Result_Management_System.Services
{
    public class BaiKiemTraService : IBaiKiemTraService
    {
        private readonly ApplicationDBContext _context;
        private readonly ICauHoiService _ICauHoiRepository;
        public BaiKiemTraService(ApplicationDBContext context, ICauHoiService ICauHoiRepository)
        {
            _context = context;
            _ICauHoiRepository = ICauHoiRepository;
        }
        public async Task<BaiKiemTraDTO> CreateBaiKiemTra(CreateBaiKiemTraDTO createBaiKiemTraDTO)
        {
            var baiKiemTra = createBaiKiemTraDTO.ToBaiKiemTraFromCreateDTO();
            await _context.BaiKiemTras.AddAsync(baiKiemTra);
            await _context.SaveChangesAsync();
            return baiKiemTra.ToBaiKiemTraDTO();
        }

        public async Task<bool> DeleteBaiKiemTra(int id)
        {
            var baiKiemTra =await _context.BaiKiemTras.FindAsync(id);
            if (baiKiemTra == null)
            {
                return false;
            }
            _context.BaiKiemTras.Remove(baiKiemTra);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BaiKiemTraDTO>> GetAllBaiKiemTra()
        {
            var baiKiemTras =await _context.BaiKiemTras.Include(c=>c.CauHois).ToListAsync();
            return baiKiemTras.Select(baiKiemTra => baiKiemTra.ToBaiKiemTraDTO()).ToList();
        }

        public async Task<List<BaiKiemTraDTO>> GetAllBaiKiemTraByLopHocPhanId(int lopHocPhanId)
        {
            var baiKiemTras =await _context.BaiKiemTras.Include(c=>c.CauHois).Where(baiKiemTra => baiKiemTra.LopHocPhanId == lopHocPhanId).ToListAsync();
            return baiKiemTras.Select(baiKiemTra => baiKiemTra.ToBaiKiemTraDTO()).ToList();
        }

        public async Task<BaiKiemTraDTO?> GetBaiKiemTra(int id)
        {
            var baiKiemTra =await _context.BaiKiemTras.Include(c=>c.CauHois).FirstOrDefaultAsync(x => x.Id == id);
            if (baiKiemTra == null)
            {
                return null;
            }
            return baiKiemTra.ToBaiKiemTraDTO();
        }

        public async Task<BaiKiemTraDTO?> UpdateBaiKiemTra(int id, UpdateBaiKiemTraDTO updateBaiKiemTraDTO)
        {
            var baiKiemTraToUpdate = await _context.BaiKiemTras.FindAsync(id);
            if (baiKiemTraToUpdate == null)
            {
                return null;
            }
            baiKiemTraToUpdate.LopHocPhanId = updateBaiKiemTraDTO.LopHocPhanId;
            baiKiemTraToUpdate.Loai = updateBaiKiemTraDTO.Loai;
            baiKiemTraToUpdate.TrongSo = updateBaiKiemTraDTO.TrongSo;
            await _context.SaveChangesAsync();
            return baiKiemTraToUpdate.ToBaiKiemTraDTO();

        }
    }
}