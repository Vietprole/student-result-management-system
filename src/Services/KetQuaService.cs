using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.KetQua;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;

namespace Student_Result_Management_System.Services
{
    public class KetQuaService : IKetQuaService
    {
        private readonly ApplicationDBContext _context;
        private readonly ICauHoiService _ICauHoiService;
        public KetQuaService(ApplicationDBContext context, ICauHoiService ICauHoiService)
        {
            _context = context;
            _ICauHoiService = ICauHoiService;
        }
        public async Task<KetQuaDTO> CreateKetQuaAsync(CreateKetQuaDTO createKetQuaDTO)
        {
            var ketQua = createKetQuaDTO.ToKetQuaFromCreateDTO();
            await _context.KetQuas.AddAsync(ketQua);
            await _context.SaveChangesAsync();
            return ketQua.ToKetQuaDTO();
        }

        public async Task<bool> DeleteKetQuaAsync(int id)
        {
            var ketQua =await _context.KetQuas.FindAsync(id);
            if (ketQua == null)
            {
                return false;
            }
            _context.KetQuas.Remove(ketQua);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<KetQuaDTO>> GetAllKetQuasAsync()
        {
            var ketQuas = await _context.KetQuas.ToListAsync();
            return ketQuas.Select(ketQua => ketQua.ToKetQuaDTO()).ToList();
        }

        public async Task<List<KetQuaDTO>> GetKetQuasByLopHocPhanIdAsync(int lopHocPhanId)
        {
            var ketQuas = await _context.KetQuas.Include(c => c.CauHois).Where(ketQua => ketQua.LopHocPhanId == lopHocPhanId).ToListAsync();
            return ketQuas.Select(ketQua => ketQua.ToKetQuaDTO()).ToList();
        }

        public async Task<KetQuaDTO?> GetKetQuaByIdAsync(int id)
        {
            var ketQua =await _context.KetQuas.Include(c=>c.CauHois).FirstOrDefaultAsync(x => x.Id == id);
            if (ketQua == null)
            {
                return null;
            }
            return ketQua.ToKetQuaDTO();
        }

        public async Task<KetQuaDTO?> UpdateKetQuaAsync(int id, UpdateKetQuaDTO updateKetQuaDTO)
        {
            var ketQuaToUpdate = await _context.KetQuas.FindAsync(id);
            if (ketQuaToUpdate == null)
            {
                return null;
            }
            ketQuaToUpdate = updateKetQuaDTO.ToKetQuaFromUpdateDTO(ketQuaToUpdate);
            await _context.SaveChangesAsync();
            return ketQuaToUpdate.ToKetQuaDTO();
        }
    }
}