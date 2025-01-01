using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.CLO;
using Student_Result_Management_System.DTOs.HocPhan;
using Student_Result_Management_System.DTOs.PLO;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Utils;
using System;

namespace Student_Result_Management_System.Services
{
    public class PLOService : IPLOService
    {
        private readonly ApplicationDBContext _context;
        public PLOService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<PLODTO>> GetAllPLOsAsync()
        {
            var plos = await _context.PLOs.Include(p => p.Nganh).ToListAsync();
            return plos.Select(plo => plo.ToPLODTO()).ToList();
        }

        public async Task<List<PLODTO>> GetPLOsByNganhIdAsync(int nganhId)
        {
            var plos = await _context.PLOs.Include(p => p.Nganh).Where(p => p.NganhId == nganhId).ToListAsync();
            return plos.Select(plo => plo.ToPLODTO()).ToList();
        }

        public async Task<List<PLODTO>> GetPLOsByLopHocPhanIdAsync(int lopHocPhanId)
        {
            var plos = await _context.PLOs.Include(p => p.Nganh).Where(p => p.HocPhans.Any(hp => hp.LopHocPhans.Any(lhp => lhp.Id == lopHocPhanId))).ToListAsync();
            return plos.Select(plo => plo.ToPLODTO()).ToList();
        }

        public async Task<List<PLODTO>> GetPLOsByHocPhanIdAsync(int hocPhanId)
        {
            var plos = await _context.PLOs.Include(p => p.Nganh).Where(p => p.HocPhans.Any(hp => hp.Id == hocPhanId)).ToListAsync();
            return plos.Select(plo => plo.ToPLODTO()).ToList();
        }

        public async Task<PLODTO?> GetPLOByIdAsync(int id)
        {
            var plo = await _context.PLOs.Include(p => p.Nganh).FirstOrDefaultAsync(p => p.Id == id);
            if (plo == null)
            {
                return null;
            }
            return plo.ToPLODTO();
        }

        public async Task<PLODTO> CreatePLOAsync(CreatePLODTO createPLODTO)
        {
            var plo = createPLODTO.ToPLOFromCreateDTO();
            await _context.PLOs.AddAsync(plo);
            await _context.SaveChangesAsync();
            return await GetPLOByIdAsync(plo.Id) ?? throw new Exception("Failed to create PLO");
        }

        public async Task<PLODTO?> UpdatePLOAsync(int id, UpdatePLODTO updatePLODTO)
        {
            var plo = await _context.PLOs.FindAsync(id);
            if (plo == null)
            {
                return null;
            }
            plo = updatePLODTO.ToPLOFromUpdateDTO(plo);
            await _context.SaveChangesAsync();
            return await GetPLOByIdAsync(id) ?? throw new Exception("Failed to update PLO");
        }

        public async Task<bool> DeletePLOAsync(int id)
        {
            var plo = await _context.PLOs.FindAsync(id);
            if (plo == null)
            {
                return false;
            }
            _context.PLOs.Remove(plo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CLODTO>> UpdateCLOsOfPLOAsync(int id, int[] cLOIds)
        {
            var pLO = await _context.PLOs
                .Include(ch => ch.CLOs)
                .FirstOrDefaultAsync(ch => ch.Id == id) ?? throw new BusinessLogicException($"PLO with id {id} not found");
            pLO.CLOs.Clear();
            foreach (var cLOId in cLOIds)
            {
                var clo = await _context.CLOs.FindAsync(cLOId) ?? throw new BusinessLogicException($"CLO with id {cLOId} not found");
                pLO.CLOs.Add(clo);
            }

            await _context.SaveChangesAsync();
            var cloList = pLO.CLOs.Select(c => c.ToCLODTO()).ToList();
            return cloList;
        }
        
        public async Task<List<HocPhanDTO>> UpdateHocPhansOfPLOAsync(int id, int[] hocPhanIds)
        {
            var pLO = await _context.PLOs
                .Include(ch => ch.HocPhans)
                .ThenInclude(hp => hp.Khoa)
                .FirstOrDefaultAsync(ch => ch.Id == id) ?? throw new BusinessLogicException($"PLO with id {id} not found");
            pLO.HocPhans.Clear();
            foreach (var hocPhanId in hocPhanIds)
            {
                var hocPhan = await _context.HocPhans.FindAsync(hocPhanId) ?? throw new BusinessLogicException($"HocPhan with id {hocPhanId} not found");
                if (hocPhan.LaCotLoi == false)
                    throw new BusinessLogicException($"HocPhan với id: {hocPhanId} không phải là cốt lõi");
                pLO.HocPhans.Add(hocPhan);
            }

            await _context.SaveChangesAsync();
            var hocPhanList = await _context.HocPhans
                .Include(hp => hp.Khoa)
                .Where(hp => hocPhanIds.Contains(hp.Id))
                .Select(hp => hp.ToHocPhanDTO())
                .ToListAsync();
            return hocPhanList;
        }
    }
}
