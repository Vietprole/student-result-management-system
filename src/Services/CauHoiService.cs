using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.CauHoi;
using Student_Result_Management_System.DTOs.CLO;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Utils;

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

        public async Task<CauHoiDTO?> UpdateCauHoiAsync(int id, UpdateCauHoiDTO updateCauHoiDTO)
        {
            var cauHoi = await _context.CauHois.FindAsync(id);
            if (cauHoi == null)
            {
                return null;
            }

            cauHoi = updateCauHoiDTO.ToCauHoiFromUpdateDTO(cauHoi);
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

        // public async Task<List<CLODTO>> AddCLOsToCauHoiAsync(int cauHoiId, int[] cLOIds)
        // {
        //     var cauHoi = await _context.CauHois
        //         .Include(ch => ch.CLOs)
        //         .FirstOrDefaultAsync(ch => ch.Id == cauHoiId);

        //     if (cauHoi == null)
        //         throw new BusinessLogicException($"CauHoi with id {cauHoiId} not found");

        //     foreach (var cLOId in cLOIds)
        //     {
        //         var clo = await _context.CLOs.FindAsync(cLOId);
        //         if (clo == null)
        //             throw new BusinessLogicException($"CLO with id {cLOId} not found");

        //         if (!cauHoi.CLOs.Contains(clo))
        //             cauHoi.CLOs.Add(clo);
        //     }

        //     await _context.SaveChangesAsync();
        //     var cloList = cauHoi.CLOs.Select(c => c.ToCLODTO()).ToList();
        //     return cloList;
        // }

        public async Task<List<CLODTO>> UpdateCLOsOfCauHoiAsync(int id, int[] cLOIds)
        {
            var cauHoi = await _context.CauHois
                .Include(ch => ch.CLOs)
                .FirstOrDefaultAsync(ch => ch.Id == id) ?? throw new BusinessLogicException($"Không tìm thấy câu hỏi với id: {id}");
            cauHoi.CLOs.Clear();
            foreach (var cLOId in cLOIds)
            {
                var clo = await _context.CLOs.FindAsync(cLOId) ?? throw new BusinessLogicException($"Không tìm thấy CLO với id: {cLOId}");
                cauHoi.CLOs.Add(clo);
            }

            await _context.SaveChangesAsync();
            var cloList = cauHoi.CLOs.Select(c => c.ToCLODTO()).ToList();
            return cloList;
        }

        public async Task<List<CauHoiDTO>> UpdateListCauHoiAsync(int baiKiemTraId, List<CreateCauHoiDTO> createCauHoiDTOs)
        {
            // Check for duplicates
            var duplicates = createCauHoiDTOs
                .GroupBy(ch => ch.Ten)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicates.Count > 0)
            {
                throw new BusinessLogicException($"Tên câu hỏi bị trùng: {string.Join(", ", duplicates)}");
            }
            
            // Validate total TrongSo equals 10
            const decimal epsilon = 0.001m;
            var totalTrongSo = createCauHoiDTOs.Sum(ch => ch.TrongSo);
            if (Math.Abs(totalTrongSo - 10m) > epsilon)
            {
                throw new BusinessLogicException($"Tổng trọng số phải bằng 10 (hiện tại là {totalTrongSo:F2})");
            }

            var baiKiemTra = await _context.BaiKiemTras
                .Include(b => b.CauHois)
                .FirstOrDefaultAsync(b => b.Id == baiKiemTraId)
                ?? throw new NotFoundException($"Không tìm thấy bài kiểm tra với id {baiKiemTraId}");

            var existingCauHois = baiKiemTra.CauHois.ToDictionary(c => c.Ten, c => c);
            var newNoiDungs = createCauHoiDTOs.Select(dto => dto.Ten).ToHashSet();

            // Update or Add CauHois
            foreach (var createDTO in createCauHoiDTOs)
            {
                if (existingCauHois.TryGetValue(createDTO.Ten, out var existingCauHoi))
                {
                    // Update existing
                    existingCauHoi.TrongSo = createDTO.TrongSo;
                    existingCauHoi.ThangDiem = createDTO.ThangDiem;
                }
                else
                {
                    // Add new
                    var newCauHoi = createDTO.ToCauHoiFromCreateDTO();
                    newCauHoi.BaiKiemTraId = baiKiemTraId;
                    baiKiemTra.CauHois.Add(newCauHoi);
                }
            }

            // Remove CauHois not in input list
            var cauHoisToRemove = baiKiemTra.CauHois
                .Where(c => !newNoiDungs.Contains(c.Ten))
                .ToList();

            foreach (var cauHoi in cauHoisToRemove)
            {
                try {
                    _context.CauHois.Remove(cauHoi);
                } catch (Exception) {
                    throw new BusinessLogicException($"Không thể xóa câu hỏi '{cauHoi.Ten}' vì đã có đối tượng con liên quan.");
                }
            }

            await _context.SaveChangesAsync();
            return baiKiemTra.CauHois.Select(c => c.ToCauHoiDTO()).ToList();
        }
    }
}