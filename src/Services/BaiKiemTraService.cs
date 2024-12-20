using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.BaiKiemTra;
using Student_Result_Management_System.DTOs.LopHocPhan;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Utils;

namespace Student_Result_Management_System.Services
{
    public class BaiKiemTraService : IBaiKiemTraService
    {
        private readonly ApplicationDBContext _context;
        public BaiKiemTraService(ApplicationDBContext context, ICauHoiService ICauHoiService)
        {
            _context = context;
            // _ICauHoiService = ICauHoiService;
        }

        public async Task<List<BaiKiemTraDTO>> GetAllBaiKiemTrasAsync()
        {
            var baiKiemTras = await _context.BaiKiemTras.Include(c => c.CauHois).ToListAsync();
            return baiKiemTras.Select(baiKiemTra => baiKiemTra.ToBaiKiemTraDTO()).ToList();
        }

        public async Task<List<BaiKiemTraDTO>> GetBaiKiemTrasByLopHocPhanIdAsync(int lopHocPhanId)
        {
            var baiKiemTras = await _context.BaiKiemTras.Include(c => c.CauHois).Where(baiKiemTra => baiKiemTra.LopHocPhanId == lopHocPhanId).ToListAsync();
            return baiKiemTras.Select(baiKiemTra => baiKiemTra.ToBaiKiemTraDTO()).ToList();
        }

        public async Task<BaiKiemTraDTO?> GetBaiKiemTraByIdAsync(int id)
        {
            var baiKiemTra =await _context.BaiKiemTras.Include(c=>c.CauHois).FirstOrDefaultAsync(x => x.Id == id);
            if (baiKiemTra == null)
            {
                return null;
            }
            return baiKiemTra.ToBaiKiemTraDTO();
        }

        public async Task<BaiKiemTraDTO> CreateBaiKiemTraAsync(CreateBaiKiemTraDTO createBaiKiemTraDTO)
        {
            var baiKiemTra = createBaiKiemTraDTO.ToBaiKiemTraFromCreateDTO();
            await _context.BaiKiemTras.AddAsync(baiKiemTra);
            await _context.SaveChangesAsync();
            return baiKiemTra.ToBaiKiemTraDTO();
        }

        public async Task<BaiKiemTraDTO?> UpdateBaiKiemTraAsync(int id, UpdateBaiKiemTraDTO updateBaiKiemTraDTO)
        {
            var baiKiemTraToUpdate = await _context.BaiKiemTras.FindAsync(id);
            if (baiKiemTraToUpdate == null)
            {
                return null;
            }
            baiKiemTraToUpdate = updateBaiKiemTraDTO.ToBaiKiemTraFromUpdateDTO(baiKiemTraToUpdate);
            await _context.SaveChangesAsync();
            return baiKiemTraToUpdate.ToBaiKiemTraDTO();
        }

        public async Task<bool> DeleteBaiKiemTraAsync(int id)
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

        public async Task<bool> CheckDuplicateBaiKiemTraLoaiInLopHocPhan(string? loai, int lopHocPhanId)
        {
            if (loai == null)
            {
                return false;
            }
            var baiKiemTra = await _context.BaiKiemTras.FirstOrDefaultAsync(x => x.Loai == loai && x.LopHocPhanId == lopHocPhanId);
            return baiKiemTra != null;
        }

        public async Task<List<BaiKiemTraDTO>?> UpdateCongThucDiem(int lopHocPhanId, List<CreateBaiKiemTraDTO> createBaiKiemTraDTOs)
        {
            var lopHocPhan = await _context.LopHocPhans
                .Include(l => l.BaiKiemTras)
                .FirstOrDefaultAsync(l => l.Id == lopHocPhanId);

            if (lopHocPhan == null)
                throw new NotFoundException($"Không tìm thấy lớp học phần với id {lopHocPhanId}");

            // Remove existing BaiKiemTras
            _context.BaiKiemTras.RemoveRange(lopHocPhan.BaiKiemTras);

            // Create new BaiKiemTras
            var newBaiKiemTras = new List<BaiKiemTra>();
            var congThucDiem = string.Empty;

            for (int i = 0; i < createBaiKiemTraDTOs.Count; i++)
            {
                var baiKiemTra = createBaiKiemTraDTOs[i].ToBaiKiemTraFromCreateDTO();
                baiKiemTra.LopHocPhanId = lopHocPhanId;
                newBaiKiemTras.Add(baiKiemTra);

                congThucDiem += baiKiemTra.Loai + "*" + baiKiemTra.TrongSo.ToString();
                if (i < createBaiKiemTraDTOs.Count - 1)
                    congThucDiem += "+";
            }

            // Add new BaiKiemTras
            await _context.BaiKiemTras.AddRangeAsync(newBaiKiemTras);
            await _context.SaveChangesAsync();

            return newBaiKiemTras.Select(b => b.ToBaiKiemTraDTO()).ToList();
        }
    }
}