using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.CauHoi;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;

namespace Student_Result_Management_System.Services
{
    public class CauHoiService : ICauHoiService
    {
        private readonly ApplicationDBContext _context;
        public CauHoiService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<CauHoiDTO>> GetAllCauHoisAsync()
        {
            var cauHois = await _context.CauHois.ToListAsync();
            return cauHois.Select(cauHoi => cauHoi.ToCauHoiDTO()).ToList();
        }

        public async Task<List<CauHoiDTO>> GetCauHoisByBaiKiemTraIdAsync(int baiKiemTraId)
        {
            var cauHois = await _context.CauHois.Where(cauHoi => cauHoi.BaiKiemTraId == baiKiemTraId).ToListAsync();
            return cauHois.Select(cauHoi => cauHoi.ToCauHoiDTO()).ToList();
        }

        public async Task<CauHoiDTO?> GetCauHoiByIdAsync(int id)
        {
            var cauHoi = await _context.CauHois.FindAsync(id);
            if (cauHoi == null)
            {
                return null;
            }
            return cauHoi.ToCauHoiDTO();
        }
        
        public async Task<CauHoiDTO> CreateCauHoiAsync(CreateCauHoiDTO createCauHoiDTO)
        {
            var cauHoi = createCauHoiDTO.ToCauHoiFromCreateDTO();
            await _context.CauHois.AddAsync(cauHoi);
            await _context.SaveChangesAsync();
            return cauHoi.ToCauHoiDTO();
        }

        public async Task<bool> DeleteCauHoiAsync(int id)
        {
            var cauHoi = await _context.CauHois.FindAsync(id);
            if (cauHoi == null)
            {
                return false;
            }
            _context.CauHois.Remove(cauHoi);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CauHoiDTO?> UpdateCauHoiAsync(int id, UpdateCauHoiDTO updateCauHoiDTO)
        {
            var cauHoi = await _context.CauHois.FindAsync(id);
            if (cauHoi == null)
            {
                return null;
            }
            cauHoi.TrongSo = updateCauHoiDTO.TrongSo;
            cauHoi.BaiKiemTraId = updateCauHoiDTO.BaiKiemTraId;
            cauHoi.Ten = updateCauHoiDTO.Ten;
            cauHoi.ThangDiem = updateCauHoiDTO.ThangDiem;
            await _context.SaveChangesAsync();
            return cauHoi.ToCauHoiDTO();
        }

        public async Task<bool> AddCLOsToCauHoiAsync(int cauHoiId, int[] cLOIds)
        {
            var cauHoi = await _context.CauHois
                .Include(ch => ch.CLOs)
                .FirstOrDefaultAsync(ch => ch.Id == cauHoiId);

            if (cauHoi == null)
            {
                return false;
            }

            foreach (var cLOId in cLOIds)
            {
                var cLO = await _context.CLOs.FindAsync(cLOId);
                if (cLO == null)
                {
                    return false;
                }

                if (!cauHoi.CLOs.Contains(cLO))
                {
                    cauHoi.CLOs.Add(cLO);
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}