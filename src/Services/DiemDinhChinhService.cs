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
        private readonly ILogger<KetQuaService> _logger;

        public DiemDinhChinhService(ApplicationDBContext context, ILogger<KetQuaService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<DiemDinhChinhDTO>> GetDiemDinhChinhsAsync(int? lopHocPhanId)
        {
            var query = _context.DiemDinhChinhs
                .Include(ddc => ddc.NguoiDuyet)
                .Include(ddc => ddc.CauHoi)
                    .ThenInclude(ch => ch.BaiKiemTra)
                    .ThenInclude(bkt => bkt.LopHocPhan)
                    .ThenInclude(lhp => lhp.GiangVien)
                    .ThenInclude(gv => gv!.TaiKhoan)
                .Include(ddc => ddc.SinhVien)
                    .ThenInclude(sv => sv.TaiKhoan)
                .AsQueryable();

            if (lopHocPhanId.HasValue)
            {
                query = query.Where(ddc => ddc.CauHoi.BaiKiemTra.LopHocPhanId == lopHocPhanId);
            }

            var diemDinhChinhs = await query.ToListAsync();
            var ketQuas = await _context.KetQuas
                .Where(k => diemDinhChinhs.Select(d => d.SinhVienId).Contains(k.SinhVienId) 
                    && diemDinhChinhs.Select(d => d.CauHoiId).Contains(k.CauHoiId))
                .ToDictionaryAsync(k => (k.SinhVienId, k.CauHoiId), k => k.DiemTam);

            return diemDinhChinhs.Select(ddc => ddc.ToDiemDinhChinhDTO(
                ketQuas.TryGetValue((ddc.SinhVienId, ddc.CauHoiId), out var diemTam) ? diemTam : null
            )).ToList();
        }

        public async Task<DiemDinhChinhDTO?> GetDiemDinhChinhByIdAsync(int id)
        {
            var diemDinhChinh = await _context.DiemDinhChinhs
                .Include(ddc => ddc.NguoiDuyet)
                .Include(ddc => ddc.CauHoi)
                    .ThenInclude(ch => ch.BaiKiemTra)
                    .ThenInclude(bkt => bkt.LopHocPhan)
                    .ThenInclude(lhp => lhp.GiangVien)
                    .ThenInclude(gv => gv!.TaiKhoan)
                .Include(ddc => ddc.SinhVien)
                    .ThenInclude(sv => sv.TaiKhoan).FirstOrDefaultAsync(x => x.Id == id);

            if (diemDinhChinh == null)
                return null;

            var ketQua = await _context.KetQuas
                .FirstOrDefaultAsync(k => 
                    k.SinhVienId == diemDinhChinh.SinhVienId && 
                    k.CauHoiId == diemDinhChinh.CauHoiId);

            return diemDinhChinh.ToDiemDinhChinhDTO(ketQua?.DiemTam);
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
            // log the diemDinhChinh
            _logger.LogInformation("DiemDinhChinh Id: {id}", id);
            _logger.LogInformation("DiemDinhChinh before update: {diemDinhChinh}", diemDinhChinh);

            diemDinhChinh = updateDTO.ToDiemDinhChinhFromUpdateDTO(diemDinhChinh);
            // log the updated diemDinhChinh
            _logger.LogInformation("DiemDinhChinh after update: {diemDinhChinh}", diemDinhChinh);
            await _context.SaveChangesAsync();

            return await GetDiemDinhChinhByIdAsync(id) ?? throw new Exception("Failed to update DiemDinhChinh");
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
                    SinhVienId = updateDTO.SinhVienId ?? throw new Exception("SinhVienId is required"),
                    CauHoiId = updateDTO.CauHoiId ?? throw new Exception("CauHoiId is required"),
                    DiemMoi = updateDTO.DiemMoi ?? 0,
                    ThoiDiemMo = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                    DuocDuyet = false
                };
                await _context.DiemDinhChinhs.AddAsync(newDiemDinhChinh);
                existingDiemDinhChinh = newDiemDinhChinh;
            }

            await _context.SaveChangesAsync();
            return await GetDiemDinhChinhByIdAsync(existingDiemDinhChinh.Id) ?? throw new Exception("Failed to upsert DiemDinhChinh");
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

        public async Task<DiemDinhChinhDTO?> AcceptDiemDinhChinhAsync(int diemDinhChinhId, int nguoiDuyetId)
        {
            var diemDinhChinh = await _context.DiemDinhChinhs.FindAsync(diemDinhChinhId) ?? throw new NotFoundException("Không tìm thấy Điểm Đính Chính");
            // Find or create KetQua
            var ketQua = await _context.KetQuas
                .FirstOrDefaultAsync(k => 
                    k.SinhVienId == diemDinhChinh.SinhVienId && 
                    k.CauHoiId == diemDinhChinh.CauHoiId);

            if (ketQua == null)
            {
                ketQua = new KetQua
                {
                    SinhVienId = diemDinhChinh.SinhVienId,
                    CauHoiId = diemDinhChinh.CauHoiId,
                    DiemTam = -1,
                    DiemChinhThuc = diemDinhChinh.DiemMoi,
                    DaCongBo = false,
                    DaXacNhan = false
                };
                await _context.KetQuas.AddAsync(ketQua);
            }
            else
            {
                ketQua.DiemChinhThuc = diemDinhChinh.DiemMoi;
            }

            // Update DiemDinhChinh
            diemDinhChinh.DuocDuyet = true;
            diemDinhChinh.NguoiDuyetId = nguoiDuyetId;
            diemDinhChinh.ThoiDiemDuyet = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

            await _context.SaveChangesAsync();
            return await GetDiemDinhChinhByIdAsync(diemDinhChinhId) ?? throw new Exception("Không thể duyệt Điểm Đính Chính");
        }
    }
}
