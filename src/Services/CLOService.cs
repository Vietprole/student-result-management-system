using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.CLO;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Utils;

namespace Student_Result_Management_System.Services;

public class CLOService : ICLOService
{
    private readonly ApplicationDBContext _context;
    public CLOService(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<List<CLO>> GetFilteredCLOsAsync(int? hocPhanId, int? hocKyId)
    {
        var query = _context.CLOs.Include(c => c.HocPhan).Include(c => c.HocKy).AsQueryable();

        if (hocPhanId.HasValue)
            query = query.Where(c => c.HocPhanId == hocPhanId.Value);

        if (hocKyId.HasValue)
            query = query.Where(c => c.HocKyId == hocKyId.Value);

        return await query.ToListAsync();
    }

    public async Task<List<CLODTO>> GetCLOsByLopHocPhanIdAsync(int lopHocPhanId)
    {
        // First find the LopHocPhan to get its HocKyId and HocPhanId
        var lopHocPhan = await _context.LopHocPhans
            .Where(lhp => lhp.Id == lopHocPhanId)
            .Select(lhp => new { lhp.HocKyId, lhp.HocPhanId })
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"Không tìm thấy lớp học phần");

        // Then query CLOs that match both HocKyId and HocPhanId
        var clos = await _context.CLOs
            .Include(c => c.HocPhan)
            .Include(c => c.HocKy)
            .Where(clo =>
                clo.HocKyId == lopHocPhan.HocKyId &&
                clo.HocPhanId == lopHocPhan.HocPhanId)
            .ToListAsync();

        return [.. clos.Select(clo => clo.ToCLODTO())];
    }

    public async Task<List<CLODTO>> GetCLOsByCauHoiIdAsync(int cauHoiId)
    {
        var clos = await _context.CLOs
            .Include(c => c.HocPhan)
            .Include(c => c.HocKy)
            .Include(c => c.CauHois)
            .Where(clo => clo.CauHois.Any(ch => ch.Id == cauHoiId))
            .ToListAsync();

        return [.. clos.Select(clo => clo.ToCLODTO())];
    }

    public async Task<List<CLODTO>> GetCLOsByPLOIdAsync(int ploId)
    {
        var clos = await _context.CLOs
            .Include(c => c.HocPhan)
            .Include(c => c.HocKy)
            .Include(c => c.PLOs)
            .Where(clo => clo.PLOs.Any(plo => plo.Id == ploId))
            .ToListAsync();
        return [.. clos.Select(clo => clo.ToCLODTO())];
    }

    public async Task<CLODTO?> GetCLOByIdAsync(int id)
    {
        var clo = await _context.CLOs
            .Include(c => c.HocPhan)
            .Include(c => c.HocKy)
            .FirstOrDefaultAsync(c => c.Id == id);
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
        var cloDTO = await GetCLOByIdAsync(clo.Id);
        return cloDTO!;
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
