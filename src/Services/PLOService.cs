using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.CLO;
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
            var plos = await _context.PLOs.ToListAsync();
            return plos.Select(plo => plo.ToPLODTO()).ToList();
        }

        public async Task<List<PLODTO>> GetPLOsByNganhIdAsync(int nganhId)
        {
            var plos = await _context.PLOs.Where(p => p.NganhId == nganhId).ToListAsync();
            return plos.Select(plo => plo.ToPLODTO()).ToList();
        }

        public async Task<List<PLODTO>> GetPLOsByLopHocPhanIdAsync(int lopHocPhanId)
        {
            var plos = await _context.PLOs.Where(p => p.HocPhans.Any(hp => hp.LopHocPhans.Any(lhp => lhp.Id == lopHocPhanId))).ToListAsync();
            return plos.Select(plo => plo.ToPLODTO()).ToList();
        }

        public async Task<List<PLODTO>> GetPLOsByHocPhanIdAsync(int hocPhanId)
        {
            var plos = await _context.PLOs.Where(p => p.HocPhans.Any(hp => hp.Id == hocPhanId)).ToListAsync();
            return plos.Select(plo => plo.ToPLODTO()).ToList();
        }

        public async Task<PLODTO?> GetPLOByIdAsync(int id)
        {
            var plo = await _context.PLOs.FindAsync(id);
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
            return plo.ToPLODTO();
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
            return plo.ToPLODTO();
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

        public async Task<ServiceResult<List<CLODTO>>> UpdateCLOsOfPLOAsync(int id, int[] cLOIds)
        {
            var pLO = await _context.PLOs
                .Include(ch => ch.CLOs)
                .FirstOrDefaultAsync(ch => ch.Id == id);

            if (pLO == null)
                return ServiceResult<List<CLODTO>>.Failure($"PLO with id {id} not found");

            pLO.CLOs.Clear();
            foreach (var cLOId in cLOIds)
            {
                var clo = await _context.CLOs.FindAsync(cLOId);
                if (clo == null)
                    return ServiceResult<List<CLODTO>>.Failure($"CLO with id {cLOId} not found");

                pLO.CLOs.Add(clo);
            }

            await _context.SaveChangesAsync();
            var cloList = pLO.CLOs.Select(c => c.ToCLODTO()).ToList();
            return ServiceResult<List<CLODTO>>.Success(cloList);
        }
    }
}
