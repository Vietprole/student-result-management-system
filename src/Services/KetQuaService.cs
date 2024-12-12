using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.KetQua;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Utils;

namespace Student_Result_Management_System.Services
{
    public class KetQuaService : IKetQuaService
    {
        private readonly ApplicationDBContext _context;
        public KetQuaService(ApplicationDBContext context, ICauHoiService ICauHoiService)
        {
            _context = context;
        }
        public async Task<KetQuaDTO> CreateKetQuaAsync(CreateKetQuaDTO createKetQuaDTO)
        {
            var ketQua = createKetQuaDTO.ToKetQuaFromCreateDTO();
            await _context.KetQuas.AddAsync(ketQua);
            await _context.SaveChangesAsync();
            return ketQua.ToKetQuaDTO();
        }

        public async Task<List<KetQuaDTO>> GetAllKetQuasAsync()
        {
            var ketQuas = await _context.KetQuas.ToListAsync();
            return ketQuas.Select(ketQua => ketQua.ToKetQuaDTO()).ToList();
        }

        // public async Task<List<KetQuaDTO>> GetKetQuasByLopHocPhanIdAsync(int lopHocPhanId)
        // {
        //     var ketQuas = await _context.KetQuas.Where(ketQua => ketQua.LopHocPhanId == lopHocPhanId).ToListAsync();
        //     return ketQuas.Select(ketQua => ketQua.ToKetQuaDTO()).ToList();
        // }

        public async Task<KetQuaDTO?> GetKetQuaByIdAsync(int id)
        {
            var ketQua = await _context.KetQuas.FirstOrDefaultAsync(x => x.Id == id);
            if (ketQua == null)
            {
                return null;
            }
            return ketQua.ToKetQuaDTO();
        }

        public async Task<KetQuaDTO?> UpdateKetQuaAsync(int id, UpdateKetQuaDTO updateKetQuaDTO)
        {
            var ketQuaToUpdate = await _context.KetQuas.FindAsync(id);
            if (ketQuaToUpdate == null)
            {
                return null;
            }
            ketQuaToUpdate = updateKetQuaDTO.ToKetQuaFromUpdateDTO(ketQuaToUpdate);
            await _context.SaveChangesAsync();
            return ketQuaToUpdate.ToKetQuaDTO();
        }

        public async Task<bool> DeleteKetQuaAsync(int id)
        {
            var ketQua = await _context.KetQuas.FindAsync(id);
            if (ketQua == null)
            {
                return false;
            }
            _context.KetQuas.Remove(ketQua);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> CalculateDiemCLO(int sinhVienId, int cLOId)
        {
            var clo = await _context.CLOs
                .Include(c => c.CauHois)
                    .ThenInclude(ch => ch.BaiKiemTra)
                .FirstOrDefaultAsync(c => c.Id == cLOId) ?? throw new BusinessLogicException("Không tìm thấy CLO");

            // Get all relevant CauHoi IDs for this CLO
            var cauHoiIds = clo.CauHois.Select(ch => ch.Id).ToList();

            // Get KetQua records and calculate weighted sum
            var ketQuas = await _context.KetQuas
                .Where(kq => kq.SinhVienId == sinhVienId && cauHoiIds.Contains(kq.CauHoiId))
                .ToListAsync();

            // Check if any DiemChinhThuc is null
            if (ketQuas.Any(kq => kq.DiemChinhThuc == null))
            {
                throw new BusinessLogicException("Một số điểm chưa phải là chính thức");
            }

            decimal totalScore = 0;
            foreach (var ketQua in ketQuas)
            {
                var cauHoi = clo.CauHois.First(ch => ch.Id == ketQua.CauHoiId);
                var diem = ketQua.DiemChinhThuc ?? 0;
                totalScore += ketQua.DiemChinhThuc ?? 0 * cauHoi.BaiKiemTra.TrongSo ?? 0;
            }

            return decimal.Round(totalScore, 2, MidpointRounding.AwayFromZero);
        }

        public async Task<decimal> CalculateDiemCLOMax(int cLOId)
        {
            var clo = await _context.CLOs
            .Include(c => c.CauHois)
                .ThenInclude(ch => ch.BaiKiemTra)
            .FirstOrDefaultAsync(c => c.Id == cLOId);

            if (clo == null)
            {
                throw new BusinessLogicException("Không tìm thấy CLO");
            }

            if (clo.CauHois.Any(c => c.BaiKiemTra.TrongSo == null))
            {
                throw new BusinessLogicException("Một số trọng số chưa được cập nhật");
            }

            decimal maxScore = 0;
            foreach (var cauHoi in clo.CauHois)
            {
                maxScore += 10m * cauHoi.TrongSo * cauHoi.BaiKiemTra.TrongSo ?? 0;
            }
            return decimal.Round(maxScore, 2, MidpointRounding.AwayFromZero);
        }

        public async Task<decimal> CalculateDiemPk(int lopHocPhanId, int sinhVienId, int ploId)
        {
            // Get LopHocPhan and related entities
            var lopHocPhan = await _context.LopHocPhans
                .Include(l => l.HocPhan)
                    .ThenInclude(h => h.PLOs.Where(p => p.Id == ploId)) // Filter PLO by Id
                        .ThenInclude(p => p.CLOs)
                            .ThenInclude(c => c.CauHois)
                                .ThenInclude(ch => ch.BaiKiemTra)
                .FirstOrDefaultAsync(l => l.Id == lopHocPhanId) ?? throw new BusinessLogicException("LopHocPhan not found");
            var plo = lopHocPhan.HocPhan.PLOs.FirstOrDefault() ?? throw new BusinessLogicException("PLO not found");

            decimal totalDiemCLO = 0;
            decimal totalMaxDiemCLO = 0;

            // For each CLO related to PLO
            foreach (var clo in plo.CLOs)
            {
                // Calculate diem-clo
                //    decimal diemClo = 0;
                //    var ketQuas = await _context.KetQuas
                //        .Where(kq => kq.SinhVienId == sinhVienId && 
                //                    clo.CauHois.Select(ch => ch.Id).Contains(kq.CauHoiId))
                //        .ToListAsync();

                //    foreach (var ketQua in ketQuas)
                //    {
                //        var cauHoi = clo.CauHois.First(ch => ch.Id == ketQua.CauHoiId);
                //        diemClo += ketQua.Diem * cauHoi.BaiKiemTra.TrongSo;
                //    }
                //    totalDiemCLO += diemClo;
                totalDiemCLO += await CalculateDiemCLO(sinhVienId, clo.Id);

                // Calculate diem-clo-max
                decimal maxDiemClo = 0;
                foreach (var cauHoi in clo.CauHois)
                {
                    maxDiemClo += 10m * cauHoi.TrongSo * cauHoi.BaiKiemTra.TrongSo ?? 0;
                }
                totalMaxDiemCLO += maxDiemClo;
            }

            decimal finalRatio = 10m * totalDiemCLO / totalMaxDiemCLO;
            return decimal.Round(finalRatio, 2, MidpointRounding.AwayFromZero);
        }

        public async Task<decimal> CalculateDiemPLO(int sinhVienId, int ploId)
        {
            decimal finalRatio = 0;
            // First verify if PLO exists
            var plo = await _context.PLOs
                .Include(p => p.HocPhans.Where(h => h.SoTinChi > 0))
                    .ThenInclude(h => h.LopHocPhans)
                        .ThenInclude(l => l.SinhViens.Where(sv => sv.Id == sinhVienId))
                .Include(p => p.CLOs)
                    .ThenInclude(c => c.CauHois)
                        .ThenInclude(ch => ch.BaiKiemTra)
                .FirstOrDefaultAsync(p => p.Id == ploId) ?? throw new BusinessLogicException("Không tìm thấy PLO");
            
            decimal totalPkMulSoTinChi = 0;
            decimal totalSoTinChi = 0;

            foreach (var hocPhan in plo.HocPhans)
            {
                decimal totalDiemCLO = 0;
                decimal totalMaxDiemCLO = 0;

                // For each CLO related to PLO
                foreach (var clo in plo.CLOs)
                {
                    // Calculate diem-clo
                    // decimal diemClo = 0;
                    // var ketQuas = await _context.KetQuas
                    //     .Where(kq => kq.SinhVienId == sinhVienId &&
                    //                 clo.CauHois.Select(ch => ch.Id).Contains(kq.CauHoiId))
                    //     .ToListAsync();

                    // foreach (var ketQua in ketQuas)
                    // {
                    //     var cauHoi = clo.CauHois.First(ch => ch.Id == ketQua.CauHoiId);
                    //     diemClo += ketQua.Diem * cauHoi.BaiKiemTra.TrongSo;
                    // }
                    var diemClo = await CalculateDiemCLO(sinhVienId, clo.Id);
                    totalDiemCLO += diemClo;

                    // Calculate diem-clo-max
                    decimal maxDiemClo = 0;
                    // foreach (var cauHoi in clo.CauHois)
                    // {
                    //     maxDiemClo += 10m * cauHoi.TrongSo * cauHoi.BaiKiemTra.TrongSo ?? 0;
                    // }
                    maxDiemClo = await CalculateDiemCLOMax(clo.Id);
                    totalMaxDiemCLO += maxDiemClo;
                }
                totalPkMulSoTinChi = 10m * totalDiemCLO / totalMaxDiemCLO * hocPhan.SoTinChi;
                totalSoTinChi += hocPhan.SoTinChi;
            }
            if (totalSoTinChi == 0)
            {
                throw new BusinessLogicException("Không tìm thấy học phần cốt lõi");
            }
            finalRatio = totalPkMulSoTinChi / totalSoTinChi;
            return decimal.Round(finalRatio, 2, MidpointRounding.AwayFromZero);
        }
    }
}