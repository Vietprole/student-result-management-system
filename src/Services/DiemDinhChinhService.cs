using System;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.DiemDinhChinh;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Utils;
using StudentResultManagementSystem.Interfaces;

namespace Student_Result_Management_System.Services
{
    public class DiemDinhChinhService : IDiemDinhChinhService
    {
        private readonly ApplicationDBContext _context;

        public DiemDinhChinhService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<DiemDinhChinhDTO>> GetDiemDinhChinhsAsync()
        {
            var diemDinhChinhs = await _context.DiemDinhChinhs.Include(ddc => ddc.NguoiDuyet).ToListAsync();
            return diemDinhChinhs.Select(ddc => ddc.ToDiemDinhChinhDTO()).ToList();
        }

        public async Task<DiemDinhChinhDTO?> GetDiemDinhChinhByIdAsync(int id)
        {
            var diemDinhChinh = await _context.DiemDinhChinhs.Include(ddc => ddc.NguoiDuyet).FirstOrDefaultAsync(x => x.Id == id);
            return diemDinhChinh?.ToDiemDinhChinhDTO();
        }

        public async Task<DiemDinhChinhDTO> CreateDiemDinhChinhAsync(CreateDiemDinhChinhDTO createDTO)
        {
            var diemDinhChinh = createDTO.ToDiemDinhChinhFromCreateDTO();
            await _context.DiemDinhChinhs.AddAsync(diemDinhChinh);
            await _context.SaveChangesAsync();
            return await GetDiemDinhChinhByIdAsync(diemDinhChinh.Id) ?? throw new Exception("Failed to create DiemDinhChinh");
        }

        public async Task<DiemDinhChinhDTO?> UpdateDiemDinhChinhAsync(int id, UpdateDiemDinhChinhDTO updateDTO)
        {
            var diemDinhChinh = await _context.DiemDinhChinhs.FindAsync(id);
            if (diemDinhChinh == null)
            {
                return null;
            }
            diemDinhChinh = updateDTO.ToDiemDinhChinhFromUpdateDTO(diemDinhChinh);
            await _context.SaveChangesAsync();
            return diemDinhChinh.ToDiemDinhChinhDTO();
        }

        public async Task<DiemDinhChinhDTO> UpsertDiemDinhChinhAsync(UpdateDiemDinhChinhDTO updateDTO)
        {
            var existingDiemDinhChinh = await _context.DiemDinhChinhs
                .FirstOrDefaultAsync(d => 
                    d.SinhVienId == updateDTO.SinhVienId && 
                    d.CauHoiId == updateDTO.CauHoiId);

            if (existingDiemDinhChinh != null)
            {
                existingDiemDinhChinh = updateDTO.ToDiemDinhChinhFromUpdateDTO(existingDiemDinhChinh);
            }
            else
            {
                var newDiemDinhChinh = new DiemDinhChinh
                {
                    SinhVienId = updateDTO.SinhVienId,
                    CauHoiId = updateDTO.CauHoiId,
                    DiemMoi = updateDTO.DiemMoi ?? 0,
                    ThoiDiemMo = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                    DuocDuyet = false
                };
                await _context.DiemDinhChinhs.AddAsync(newDiemDinhChinh);
                existingDiemDinhChinh = newDiemDinhChinh;
            }

            await _context.SaveChangesAsync();
            return existingDiemDinhChinh.ToDiemDinhChinhDTO();
        }

        public async Task<bool> DeleteDiemDinhChinhAsync(int id)
        {
            var diemDinhChinh = await _context.DiemDinhChinhs.FindAsync(id);
            if (diemDinhChinh == null)
            {
                return false;
            }
            _context.DiemDinhChinhs.Remove(diemDinhChinh);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
