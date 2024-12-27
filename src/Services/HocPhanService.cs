using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class HocPhanService : IHocPhanService
    {
        private readonly ApplicationDBContext _context;
        public HocPhanService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<HocPhanDTO>> GetAllHocPhansAsync()
        {
            var hocPhans = await _context.HocPhans.Include(hp => hp.Khoa).ToListAsync();
            return hocPhans.Select(hocPhan => hocPhan.ToHocPhanDTO()).ToList();
        }

        public async Task<List<HocPhanDTO>> GetHocPhansByKhoaIdAsync(int khoaId)
        {
            var hocPhans = await _context.HocPhans.Include(hp => hp.Khoa).Where(hocPhan => hocPhan.KhoaId == khoaId).ToListAsync();
            return hocPhans.Select(hocPhan => hocPhan.ToHocPhanDTO()).ToList();
        }

        public async Task<List<HocPhanDTO>> GetHocPhansByPLOIdAsync(int pLOId)
        {
            var hocPhans = await _context.HocPhans.Include(hp => hp.Khoa).Include(hp => hp.PLOs).Where(hocPhan => hocPhan.PLOs.Any(plo => plo.Id == pLOId)).ToListAsync();
            return hocPhans.Select(hocPhan => hocPhan.ToHocPhanDTO()).ToList();
        }

        public async Task<List<HocPhanDTO>> GetFilteredHocPhansAsync(int? khoaId, int? nganhId, bool? laCotLoi)
        {
            IQueryable<HocPhan> query = _context.HocPhans.Include(hp => hp.Khoa) // Include the Khoa navigation property
                .Include(hp => hp.Nganhs); // Include the Nganhs navigation property

            if (khoaId.HasValue)
            {
                query = query.Where(hp => hp.KhoaId == khoaId.Value);
            }

            if (nganhId.HasValue)
            {
                query = query.Where(hp => hp.Nganhs.Any(n => n.Id == nganhId.Value));
            }

            if (laCotLoi.HasValue)
            {
                query = query.Where(hp => hp.LaCotLoi == laCotLoi.Value);
            }

            var hocPhans = await query.ToListAsync();
            return hocPhans.Select(hp => hp.ToHocPhanDTO()).ToList();
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
            int soluong = await _context.HocPhans.CountAsync()+1;
            while(true)
            {
                if(await _context.HocPhans.AnyAsync(hp => hp.MaHocPhan == khoa.MaKhoa + soluong.ToString("D4")))
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