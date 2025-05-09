using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.HocPhan;
using Student_Result_Management_System.DTOs.PLO;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Utils;

namespace Student_Result_Management_System.Services
{
    public class HocPhanService(ApplicationDBContext context) : IHocPhanService
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<List<HocPhan>> GetFilteredHocPhansAsync(int? khoaId, int? nganhId, int? pLOId, int? pageNumber, int? pageSize)
        {
            var query = _context.HocPhans.Include(hp => hp.Khoa).AsQueryable();

            if (khoaId.HasValue)
                query = query.Where(hp => hp.KhoaId == khoaId.Value);

            if (nganhId.HasValue)
                query = query.Where(hp => hp.Nganhs.Any(n => n.Id == nganhId.Value));

            if (pLOId.HasValue)
                query = query.Include(hp => hp.PLOs).Where(hocPhan => hocPhan.PLOs.Any(plo => plo.Id == pLOId));

            // Apply pagination
            query = query.ApplyPagination(pageNumber, pageSize);

            return await query.ToListAsync();
        }

        public async Task<HocPhanDTO?> GetHocPhanByIdAsync(int id)
        {
            var hocPhan = await _context.HocPhans.Include(hp => hp.Khoa).FirstOrDefaultAsync(hp => hp.Id == id);
            if (hocPhan == null)
            {
                return null;
            }
            return hocPhan.ToHocPhanDTO();
        }

        public async Task<HocPhanDTO?> CreateHocPhanAsync(CreateHocPhanDTO createHocPhanDTO)
        {
            var hocPhan = createHocPhanDTO.ToHocPhanFromCreateDTO();
            var khoa = await _context.Khoas.FindAsync(createHocPhanDTO.KhoaId);
            if (khoa == null)
            {
                return null;
            }
            int soluong = await _context.HocPhans.CountAsync() + 1;
            while (true)
            {
                if (await _context.HocPhans.AnyAsync(hp => hp.MaHocPhan == khoa.MaKhoa + soluong.ToString("D4")))
                {
                    soluong++;
                }
                else
                {
                    break;
                }
            }
            hocPhan.MaHocPhan = khoa.MaKhoa + soluong.ToString("D4");
            await _context.HocPhans.AddAsync(hocPhan);
            await _context.SaveChangesAsync();
            return hocPhan.ToHocPhanDTO();
        }

        public async Task<HocPhanDTO?> UpdateHocPhanAsync(int id, UpdateHocPhanDTO updateHocPhanDTO)
        {
            var hocPhan = await _context.HocPhans.Include(hp => hp.Khoa).FirstOrDefaultAsync(hp => hp.Id == id);
            if (hocPhan == null)
            {
                return null;
            }

            hocPhan = updateHocPhanDTO.ToHocPhanFromUpdateDTO(hocPhan);
            await _context.SaveChangesAsync();
            return hocPhan.ToHocPhanDTO();
        }

        public async Task<bool> DeleteHocPhanAsync(int id)
        {
            var hocPhan = await _context.HocPhans.FindAsync(id);
            if (hocPhan == null)
            {
                return false;
            }
            _context.HocPhans.Remove(hocPhan);
            await _context.SaveChangesAsync();
            return true;
        }

        // public async Task<List<PLODTO>> AddPLOsToHocPhanAsync(int hocPhanId, int[] pLOIds)
        // {
        //     var hocPhan = await _context.HocPhans
        //         .Include(ch => ch.PLOs)
        //         .FirstOrDefaultAsync(ch => ch.Id == hocPhanId);

        //     if (hocPhan == null)
        //         throw new BusinessLogicException($"HocPhan with id {hocPhanId} not found");

        //     foreach (var pLOId in pLOIds)
        //     {
        //         var plo = await _context.PLOs.FindAsync(pLOId);
        //         if (plo == null)
        //             throw new BusinessLogicException($"PLO with id {pLOId} not found");

        //         if (!hocPhan.PLOs.Contains(plo))
        //             hocPhan.PLOs.Add(plo);
        //     }

        //     await _context.SaveChangesAsync();
        //     var ploList = hocPhan.PLOs.Select(c => c.ToPLODTO()).ToList();
        //     return ploList;
        // }

        public async Task<List<PLODTO>> UpdatePLOsOfHocPhanAsync(int id, int[] pLOIds)
        {
            var hocPhan = await _context.HocPhans
                .Include(ch => ch.PLOs)
                .FirstOrDefaultAsync(ch => ch.Id == id) ?? throw new BusinessLogicException($"Không tìm thấy học phần với id: {id}");
            hocPhan.PLOs.Clear();
            foreach (var pLOId in pLOIds)
            {
                var plo = await _context.PLOs.FindAsync(pLOId) ?? throw new BusinessLogicException($"Không tìm thấy PLO với id: {pLOId}");
                hocPhan.PLOs.Add(plo);
            }

            await _context.SaveChangesAsync();
            var ploList = hocPhan.PLOs.Select(c => c.ToPLODTO()).ToList();
            return ploList;
        }

        public async Task<List<HocPhan>> GetAllHocPhanNotInNganhId(int nganhId)
        {
            // Lấy danh sách các học phần không thuộc ngành có Id là nganhId
            var hocPhansNotInNganh = await _context.HocPhans
                .Where(hp => !hp.Nganhs.Any(n => n.Id == nganhId))
                .ToListAsync();

            return hocPhansNotInNganh;
        }

    }
}