using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.HocPhan;
using Student_Result_Management_System.DTOs.Nganh;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Utils;

namespace Student_Result_Management_System.Services
{
    public class NganhService : INganhService
    {
        private readonly ApplicationDBContext _context;
        public NganhService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateMaNganhAsync(int khoaId)
        {
            // Get MaKhoa
            var khoa = await _context.Khoas.FindAsync(khoaId) ?? throw new NotFoundException($"Không tìm thấy Khoa với Id: {khoaId}");
            string maKhoa = khoa.MaKhoa;

            // Get existing Nganh count for this Khoa
            int existingNganhCount = await _context.Nganhs
                .CountAsync(n => n.KhoaId == khoaId);

            // Calculate next sequential number
            int nextSequentialNumber = existingNganhCount + 1;
            if (nextSequentialNumber > 9999)
                throw new BusinessLogicException("Đã đạt đến số lượng Ngành tối đa cho Khoa này");

            // Combine MaKhoa and sequential number
            string maNganh = $"{maKhoa}{nextSequentialNumber:D4}";

            return maNganh;
        }

        public async Task<List<Nganh>> GetFilteredNganhsAsync(int? khoaId, int? nguoiQuanLyId, int? pageNumber, int? pageSize)
        {
            var query = _context.Nganhs.Include(n => n.Khoa).Include(n => n.TaiKhoan).AsQueryable();

            // Apply filtering
            if (khoaId.HasValue)
                query = query.Where(n => n.KhoaId == khoaId.Value);

            if (nguoiQuanLyId.HasValue)
                query = query.Where(n => n.TaiKhoanId == nguoiQuanLyId.Value);

            // Apply pagination using the utility function
            query = query.ApplyPagination(pageNumber, pageSize);

            return await query.ToListAsync();
        }

        public async Task<Nganh?> GetNganhByIdAsync(int id)
        {
            return await _context.Nganhs
                .Include(n => n.Khoa)
                .Include(n => n.TaiKhoan)
                .FirstOrDefaultAsync(n => n.Id == id);
        }
        public async Task<Nganh> CreateNganhAsync(Nganh nganh)
        {
            nganh.MaNganh = await GenerateMaNganhAsync(nganh.KhoaId);
            await _context.Nganhs.AddAsync(nganh);
            await _context.SaveChangesAsync();
            return await GetNganhByIdAsync(nganh.Id) ?? throw new InvalidOperationException("Failed to retrieve the created Nganh.");
        }

        public async Task<Nganh?> UpdateNganhAsync(int id, UpdateNganhDTO updateNganhDTO)
        {
            var nganh = await _context.Nganhs.FindAsync(id) ??
                throw new NotFoundException("Không tìm thấy Ngành");

            nganh = updateNganhDTO.ToNganhFromUpdateDTO(nganh);
            await _context.SaveChangesAsync();
            return await GetNganhByIdAsync(id);
        }

        public async Task<bool> DeleteNganhAsync(int id)
        {
            var nganh = await _context.Nganhs.FindAsync(id) ??
                throw new NotFoundException("Không tìm thấy Ngành");
            try
            {
                _context.Nganhs.Remove(nganh);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw new BusinessLogicException("Ngành chứa các đối tượng con, không thể xóa");
            }
        }

        public async Task<List<HocPhanDTO>> AddHocPhansToNganhAsync(int nganhId, int[] hocPhanIds)
        {
            var nganh = await _context.Nganhs
                .Include(n => n.HocPhans)
                .FirstOrDefaultAsync(n => n.Id == nganhId) ?? throw new NotFoundException($"Không tìm thấy Ngành với id: {nganhId}");

            foreach (var hocPhanId in hocPhanIds)
            {
                var hocPhan = await _context.HocPhans.FindAsync(hocPhanId);
                if (hocPhan == null)
                {
                    // Nếu không tìm thấy học phần, có thể thêm một thông báo lỗi cụ thể
                    throw new NotFoundException($"Không tìm thấy Học Phần với id: {hocPhanId}");
                }

                if (!nganh.HocPhans.Contains(hocPhan))
                    nganh.HocPhans.Add(hocPhan);
            }

            await _context.SaveChangesAsync();
            return await GetHocPhansInNganhAsync(nganhId);
        }


        public async Task<List<HocPhanDTO>> UpdateHocPhansOfNganhAsync(int nganhId, int[] hocPhanIds)
        {
            var nganh = await _context.Nganhs
                .Include(n => n.HocPhans)
                .FirstOrDefaultAsync(n => n.Id == nganhId) ?? throw new NotFoundException($"Không tìm thấy Ngành với id: {nganhId}");

            nganh.HocPhans.Clear();
            foreach (var hocPhanId in hocPhanIds)
            {
                var hocPhan = await _context.HocPhans.FindAsync(hocPhanId) ?? throw new NotFoundException($"Không tìm thấy Học Phần với id: {hocPhanId}");
                nganh.HocPhans.Add(hocPhan);
            }

            await _context.SaveChangesAsync();
            return await GetHocPhansInNganhAsync(nganhId);
        }

        public async Task<bool> RemoveHocPhanFromNganhAsync(int nganhId, int hocPhanId)
        {
            var nganh = await _context.Nganhs
                .Include(n => n.HocPhans)
                .FirstOrDefaultAsync(n => n.Id == nganhId) ?? throw new NotFoundException($"Không tìm thấy Ngành với id: {nganhId}");

            var hocPhan = await _context.HocPhans.FindAsync(hocPhanId) ?? throw new NotFoundException($"Không tìm thấy Học Phần với id: {hocPhanId}");

            if (!nganh.HocPhans.Contains(hocPhan))
                return false;

            nganh.HocPhans.Remove(hocPhan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<HocPhanDTO>> GetHocPhansInNganhAsync(int nganhId)
        {
            var nganh = await _context.Nganhs
                .Include(n => n.Khoa)
                .Include(n => n.HocPhans)
                    .ThenInclude(hp => hp.Khoa)
                .Include(n => n.Ctdts)
                .FirstOrDefaultAsync(n => n.Id == nganhId) ?? throw new NotFoundException($"Không tìm thấy Ngành với id: {nganhId}");

            return nganh.HocPhans.Select(hp => new HocPhanDTO
            {
                Id = hp.Id,
                MaHocPhan = hp.MaHocPhan,
                Ten = hp.Ten,
                SoTinChi = hp.SoTinChi,
                KhoaId = hp.KhoaId,
                TenKhoa = hp.Khoa.Ten,
                LaCotLoi = nganh.Ctdts.Any(c => c.HocPhanId == hp.Id && c.LaCotLoi) // Nếu có ít nhất một cái là true, trả về true
            }).ToList();
        }


        public async Task<List<HocPhanDTO>> UpdateHocPhanCotLoi(int nganhId, List<UpdateCotLoiDTO> updateCotLoiDTOs)
        {
            var nganh = await _context.Nganhs
                .Include(n => n.Ctdts)
                .FirstOrDefaultAsync(n => n.Id == nganhId) ?? throw new NotFoundException($"Không tìm thấy Ngành với id: {nganhId}");

            foreach (var updateCotLoiDTO in updateCotLoiDTOs)
            {
                var ctdt = nganh.Ctdts.SingleOrDefault(c => c.HocPhanId == updateCotLoiDTO.HocPhanId) ?? throw new NotFoundException($"Không tìm thấy Học phần với id: {updateCotLoiDTO.HocPhanId} trong Ngành");
                ctdt.LaCotLoi = updateCotLoiDTO.LaCotLoi;
            }

            await _context.SaveChangesAsync();
            return await GetHocPhansInNganhAsync(nganhId);
        }

        public async Task<bool> CheckNganhExits(string tenNganh, int khoaId)
        {
            var nganhs = await _context.Nganhs
                .Where(n => n.KhoaId == khoaId)
                .ToListAsync();
            return nganhs.Any(n => n.Ten == tenNganh);
        }

        public async Task CopyNganhStructureAsync(int targetNganhId, int sourceNganhId)
        {
            if (targetNganhId == sourceNganhId)
                throw new BusinessLogicException("Không thể kế thừa Ngành vào chính nó");

            var targetnganh = await _context.Nganhs
                .FirstOrDefaultAsync(n => n.Id == targetNganhId) ?? throw new NotFoundException($"Không tìm thấy Ngành với id: {targetNganhId}");

            var sourceNganh = await _context.Nganhs
                .FirstOrDefaultAsync(n => n.Id == sourceNganhId) ?? throw new NotFoundException($"Không tìm thấy Ngành nguồn với id: {sourceNganhId}");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await CopyHocPhansAsync(targetNganhId, sourceNganhId);
                await CopyPLOsAsync(targetNganhId, sourceNganhId);
                await CopyHocPhanPLOMappingAsync(targetNganhId, sourceNganhId);
                await CopyPLOCLOMappingAsync(targetNganhId, sourceNganhId);
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw new BusinessLogicException("Lỗi khi sao chép cấu trúc Ngành");
            }
        }

        private async Task CopyHocPhansAsync(int targetNganhId, int sourceNganhId)
        {
            // Get current Ctdts for target Nganh
            var targetCtdts = await _context.Ctdts
                .Where(c => c.NganhId == targetNganhId)
                .ToListAsync();

            // Get source Ctdts
            var sourceCtdts = await _context.Ctdts
                .Where(c => c.NganhId == sourceNganhId)
                .ToListAsync();

            // Remove existing Ctdts from target
            _context.Ctdts.RemoveRange(targetCtdts);

            // Add new Ctdts based on source
            var newCtdts = sourceCtdts.Select(c => new Ctdt
            {
                NganhId = targetNganhId,
                HocPhanId = c.HocPhanId,
                LaCotLoi = c.LaCotLoi
            }).ToList();

            await _context.Ctdts.AddRangeAsync(newCtdts);
            await _context.SaveChangesAsync();
        }

        private async Task CopyPLOsAsync(int targetNganhId, int sourceNganhId)
        {
            // Get current PLOs for target Nganh
            var targetPLOs = await _context.PLOs
                .Where(p => p.NganhId == targetNganhId)
                .ToListAsync();

            // Get source PLOs
            var sourcePLOs = await _context.PLOs
                .Where(p => p.NganhId == sourceNganhId)
                .ToListAsync();

            // Remove existing PLOs from target
            _context.PLOs.RemoveRange(targetPLOs);

            // Add new PLOs based on source
            var newPLOs = sourcePLOs.Select(p => new PLO
            {
                MoTa = p.MoTa,
                Ten = p.Ten,
                NganhId = targetNganhId,
            }).ToList();

            await _context.PLOs.AddRangeAsync(newPLOs);
            await _context.SaveChangesAsync();
        }

        private async Task CopyHocPhanPLOMappingAsync(int targetNganhId, int sourceNganhId)
        {
            var sourcePLOs = await _context.PLOs
                .Include(p => p.HocPhans)
                .Where(p => p.NganhId == sourceNganhId)
                .ToListAsync();

            var targetPLOs = await _context.PLOs
                .Include(p => p.HocPhans)
                .Where(p => p.NganhId == targetNganhId)
                .ToListAsync();

            foreach (var targetPLO in targetPLOs)
            {
                // Clear existing HocPhans in target PLO
                targetPLO.HocPhans.Clear();
                // Find the corresponding source PLO (match by name or other criteria)
                var sourcePLO = sourcePLOs.FirstOrDefault(p => p.Ten == targetPLO.Ten);

                if (sourcePLO == null)
                    continue;

                // Get all HocPhans from source PLO
                var hocPhanIdsFromSource = sourcePLO.HocPhans.Select(h => h.Id).ToList();

                // Get these actual HocPhan entities (avoid creating new ones)
                var hocPhansToAdd = await _context.HocPhans
                    .Where(h => hocPhanIdsFromSource.Contains(h.Id))
                    .ToListAsync();

                // Add each HocPhan to the target PLO's collection
                foreach (var hocPhan in hocPhansToAdd)
                {
                    // This will create entries in the HocPhanPLO join table
                    targetPLO.HocPhans.Add(hocPhan);
                }
            }

            // Save changes to persist the mapping
            await _context.SaveChangesAsync();
        }

        private async Task CopyPLOCLOMappingAsync(int targetNganhId, int sourceNganhId)
        {
            var sourcePLOs = await _context.PLOs
                .Include(p => p.CLOs)
                .Where(p => p.NganhId == sourceNganhId)
                .ToListAsync();

            var targetPLOs = await _context.PLOs
                .Include(p => p.CLOs)
                .Where(p => p.NganhId == targetNganhId)
                .ToListAsync();

            foreach (var targetPLO in targetPLOs)
            {
                // Clear existing CLOs in target PLO
                targetPLO.CLOs.Clear();
                // Find the corresponding source PLO (match by name or other criteria)
                var sourcePLO = sourcePLOs.FirstOrDefault(p => p.Ten == targetPLO.Ten);

                if (sourcePLO == null)
                    continue;

                // Get all CLOs from source PLO
                var cloIdsFromSource = sourcePLO.CLOs.Select(c => c.Id).ToList();

                // Get these actual CLO entities (avoid creating new ones)
                var closToAdd = await _context.CLOs
                    .Where(c => cloIdsFromSource.Contains(c.Id))
                    .ToListAsync();

                // Add each CLO to the target PLO's collection
                foreach (var clo in closToAdd)
                {
                    // This will create entries in the CLO PLO join table
                    targetPLO.CLOs.Add(clo);
                }
            }

            // Save changes to persist the mapping
            await _context.SaveChangesAsync();
        }
    }
}