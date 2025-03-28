using System;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.CLO;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;

namespace Student_Result_Management_System.Services;

public class CLOService : ICLOService
{
    private readonly ApplicationDBContext _context;
    public CLOService(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<List<CLODTO>> GetAllCLOsAsync()
    {
        var clos = await _context.CLOs.ToListAsync();
        return clos.Select(clo => clo.ToCLODTO()).ToList();
    }

    public async Task<List<CLODTO>> GetCLOsByLopHocPhanIdAsync(int lopHocPhanId)
    {
        var clos = await _context.CLOs
            .Include(c => c.LopHocPhans)
            .Where(clo => clo.LopHocPhans.Any(lhp => lhp.Id == lopHocPhanId))
            .ToListAsync();
        return [.. clos.Select(clo => clo.ToCLODTO())];
    }

    public async Task<List<CLODTO>> GetCLOsByCauHoiIdAsync(int cauHoiId)
    {
        var clos = await _context.CLOs
            .Include(c => c.CauHois)
            .Where(clo => clo.CauHois.Any(ch => ch.Id == cauHoiId))
            .ToListAsync();
            
        return clos.Select(clo => clo.ToCLODTO()).ToList();
    }

    public async Task<List<CLODTO>> GetCLOsByPLOIdAsync(int ploId)
    {
        var clos = await _context.CLOs
            .Include(c => c.PLOs)
            .Where(clo => clo.PLOs.Any(plo => plo.Id == ploId))
            .ToListAsync();
        return clos.Select(clo => clo.ToCLODTO()).ToList();
    }

    public async Task<CLODTO?> GetCLOByIdAsync(int id)
    {
        var clo = await _context.CLOs.FindAsync(id);
        if (clo == null)
        {
            return null;
        }
        return clo.ToCLODTO();
    }

    public async Task<CLODTO> CreateCLOAsync(CreateCLODTO createCLODTO)
    {
        var clo = createCLODTO.ToCLOFromCreateDTO();
        await _context.CLOs.AddAsync(clo);
        await _context.SaveChangesAsync();
        return clo.ToCLODTO();
    }

    public async Task<CLODTO?> UpdateCLOAsync(int id, UpdateCLODTO updateCLODTO)
    {
        var clo = await _context.CLOs.FindAsync(id);
        if (clo == null)
        {
            return null;
        }
        clo = updateCLODTO.ToCLOFromUpdateDTO(clo);
        await _context.SaveChangesAsync();
        return clo.ToCLODTO();
    }

    public async Task<bool> DeleteCLOAsync(int id)
    {
        var clo = await _context.CLOs.FindAsync(id);
        if (clo == null)
        {
            return false;
        }
        _context.CLOs.Remove(clo);
        await _context.SaveChangesAsync();
        return true;
    }
}
