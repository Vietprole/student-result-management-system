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
        private readonly ICauHoiService _cauHoiService;
        private readonly ILogger<KetQuaService> _logger;
        public KetQuaService(ApplicationDBContext context, ICauHoiService cauHoiService, ILogger<KetQuaService> logger)
        {
            _context = context;
            _cauHoiService = cauHoiService;
            _logger = logger;
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

        public async Task<List<KetQuaDTO>> GetFilteredKetQuasAsync(int? baiKiemTraId, int? sinhVienId, int? lopHocPhanId)
        {
            IQueryable<KetQua> query = _context.KetQuas
                .Include(kq => kq.CauHoi)
                    .ThenInclude(ch => ch.BaiKiemTra)
                    .ThenInclude(bkt => bkt.LopHocPhan)
                .Include(kq => kq.SinhVien);

            if (baiKiemTraId.HasValue)
            {
                query = query.Where(kq => kq.CauHoi.BaiKiemTraId == baiKiemTraId.Value);
            }

            if (sinhVienId.HasValue)
            {
                query = query.Where(kq => kq.SinhVienId == sinhVienId.Value);
            }

            if (lopHocPhanId.HasValue)
            {
                query = query.Where(kq => kq.CauHoi.BaiKiemTra.LopHocPhanId == lopHocPhanId.Value);
            }

            var ketQuas = await query.ToListAsync();
            return ketQuas.Select(hp => hp.ToKetQuaDTO()).ToList();
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

        public async Task<KetQuaDTO> UpsertKetQuaAsync(UpdateKetQuaDTO ketQuaDTO)
        {
            var existingKetQua = await _context.KetQuas.FirstOrDefaultAsync(k =>
                k.SinhVienId == ketQuaDTO.SinhVienId &&
                k.CauHoiId == ketQuaDTO.CauHoiId);

            if (existingKetQua != null)
            {
                // Update
                existingKetQua = ketQuaDTO.ToKetQuaFromUpdateDTO(existingKetQua);
            }
            else
            {
                // Create
                var newKetQua = new KetQua
                {
                    SinhVienId = ketQuaDTO.SinhVienId ?? throw new BusinessLogicException("SinhVienId là bắt buộc"),
                    CauHoiId = ketQuaDTO.CauHoiId ?? throw new BusinessLogicException("CauHoiId là bắt buộc"),
                    DiemTam = ketQuaDTO.DiemTam,
                    DiemChinhThuc = ketQuaDTO.DiemChinhThuc,
                    DaCongBo = false
                };
                await _context.KetQuas.AddAsync(newKetQua);
                existingKetQua = newKetQua;
            }

            await _context.SaveChangesAsync();
            return existingKetQua.ToKetQuaDTO();
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

        public async Task<KetQuaDTO?> ConfirmKetQuaAsync(ConfirmKetQuaDTO confirmKetQuaDTO)
        {
            var existingKetQua = await _context.KetQuas.Include(k => k.CauHoi).ThenInclude(c => c.BaiKiemTra).FirstOrDefaultAsync(k =>
                k.SinhVienId == confirmKetQuaDTO.SinhVienId &&
                k.CauHoiId == confirmKetQuaDTO.CauHoiId) ?? throw new NotFoundException(
                    $"Không tìm thấy kết quả với SinhVienId={confirmKetQuaDTO.SinhVienId} và CauHoiId={confirmKetQuaDTO.CauHoiId}");
            
            if (existingKetQua.DiemTam == null)
            {
                throw new BusinessLogicException("Chưa nhập điểm");
            }

            if (existingKetQua.DaXacNhan == true)
            {
                throw new BusinessLogicException("Điểm này đã được xác nhận");
            }
            
            existingKetQua.DaXacNhan = true;
            await _context.SaveChangesAsync();
            // Add date of confirmation to baiKiemTra
            var baiKiemTraId = existingKetQua.CauHoi.BaiKiemTraId;
            var relatedKetQuas = await _context.KetQuas
                .Where(k => k.CauHoi.BaiKiemTraId == baiKiemTraId)
                .ToListAsync();

            // Check if all related KetQuas are confirmed
            if (relatedKetQuas.All(k => k.DaXacNhan))
            {
                existingKetQua.CauHoi.BaiKiemTra.NgayXacNhan = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
            }
            await _context.SaveChangesAsync();
            
            return existingKetQua.ToKetQuaDTO();
        }

        public async Task<KetQuaDTO?> AcceptKetQuaAsync(AcceptKetQuaDTO acceptKetQuaDTO)
        {
            var existingKetQua = await _context.KetQuas.FirstOrDefaultAsync(k =>
                k.SinhVienId == acceptKetQuaDTO.SinhVienId &&
                k.CauHoiId == acceptKetQuaDTO.CauHoiId) ?? throw new NotFoundException(
                    $"Không tìm thấy kết quả với SinhVienId={acceptKetQuaDTO.SinhVienId} và CauHoiId={acceptKetQuaDTO.CauHoiId}");

            if (existingKetQua.DiemChinhThuc != null)
            {
                throw new BusinessLogicException("Điểm này đã được duyệt");
            }
            existingKetQua.DiemChinhThuc = existingKetQua.DiemTam;
            await _context.SaveChangesAsync();
            return existingKetQua.ToKetQuaDTO();
        }

        public async Task<decimal> CalculateDiemCLO(int sinhVienId, int cLOId, bool useDiemTam = false)
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

            decimal totalScore = 0;
            bool useTemporaryScores = useDiemTam;

            foreach (var ketQua in ketQuas)
            {
                var cauHoi = clo.CauHois.First(ch => ch.Id == ketQua.CauHoiId);
                decimal diem = useTemporaryScores ? (ketQua.DiemTam ?? 0m) : (ketQua.DiemChinhThuc ?? 0m);
                decimal trongSoCauHoi = cauHoi.TrongSo;
                decimal trongSoBaiKiemTra = cauHoi.BaiKiemTra?.TrongSo ?? 0m;
                decimal thangDiem = cauHoi.ThangDiem;

                var score = (diem / thangDiem) * trongSoCauHoi * trongSoBaiKiemTra;
                totalScore += score;
                // _logger.LogInformation(
                //     "CauHoi {CauHoiId}: Diem={Diem}, ThangDiem={ThangDiem}, TrongSoCH={TrongSoCH}, TrongSoBKT={TrongSoBKT}, Score={Score}",
                //     cauHoi.Id,
                //     diem,
                //     thangDiem,
                //     trongSoCauHoi,
                //     trongSoBaiKiemTra,
                //     score
                // );
            }
            // _logger.LogInformation("Final Score for SinhVien {SinhVienId}, CLO {CLOId}: {Score}",
            //     sinhVienId,
            //     cLOId,
            //     totalScore);

            return decimal.Round(totalScore, 2, MidpointRounding.AwayFromZero);
        }

        public async Task<decimal> CalculateDiemCLOMax(int cLOId)
        {
            var clo = await _context.CLOs
            .Include(c => c.CauHois)
                .ThenInclude(ch => ch.BaiKiemTra)
            .FirstOrDefaultAsync(c => c.Id == cLOId) ?? throw new BusinessLogicException("Không tìm thấy CLO");
            
            if (clo.CauHois.Count == 0)
            {
                throw new BusinessLogicException("CLO chưa có câu hỏi nào");
            }

            if (clo.CauHois.Any(c => c.BaiKiemTra.TrongSo == null))
            {
                throw new BusinessLogicException("Một số trọng số chưa được cập nhật");
            }

            decimal maxScore = 0;
            foreach (var cauHoi in clo.CauHois)
            {
                maxScore += cauHoi.TrongSo * cauHoi.BaiKiemTra.TrongSo ?? 0 / cauHoi.ThangDiem;
            }
            return decimal.Round(maxScore, 2, MidpointRounding.AwayFromZero);
        }

        public async Task<decimal> CalculateDiemPk(int lopHocPhanId, int sinhVienId, int ploId, bool useDiemTam = false)
        {
            var lopHocPhan = await _context.LopHocPhans
                .Include(l => l.HocPhan)
                    .ThenInclude(h => h.PLOs.Where(p => p.Id == ploId)) // Filter PLO by Id
                .Include(l => l.CLOs)
                .FirstOrDefaultAsync(l => l.Id == lopHocPhanId) ?? throw new NotFoundException("Không tìm thấy lớp học phần");
            
            // if (!lopHocPhan.HocPhan.LaCotLoi)
            // {
            //     throw new BusinessLogicException("Chỉ được tính điểm Pk cho học phần cốt lõi");
            // }
            var plo = lopHocPhan.HocPhan.PLOs.FirstOrDefault(p => p.Id == ploId) ?? throw new NotFoundException("Không tìm thấy PLO");
            //var cloItem = lopHocPhan.HocPhan.PLOs.FirstOrDefault(p => p.Id == ploId)?.CLOs.FirstOrDefault(c => c.LopHocPhanId == lopHocPhanId) ?? throw new NotFoundException("Không tìm thấy CLO");
            var cloItem = lopHocPhan.CLOs.FirstOrDefault(c => c.PLOs.Any(p => p.Id == ploId)) ?? throw new NotFoundException("Không tìm thấy CLO");

            var filteredCLOs = lopHocPhan.CLOs.Where(c => c.PLOs.Any(p => p.Id == ploId));
            decimal totalDiemCLO = 0;
            decimal totalMaxDiemCLO = 0;

            // For each CLO related to PLO
            foreach (var clo in filteredCLOs)
            {
                totalDiemCLO += await CalculateDiemCLO(sinhVienId, clo.Id, useDiemTam);
                totalMaxDiemCLO += await CalculateDiemCLOMax(clo.Id);
            }

            decimal finalRatio = 10m * totalDiemCLO / totalMaxDiemCLO;
            return decimal.Round(finalRatio, 2, MidpointRounding.AwayFromZero);
        }

        public async Task<decimal> CalculateDiemPLO(int sinhVienId, int ploId, bool useDiemTam = false)
        {
            decimal finalRatio = 0;
            // First verify if PLO exists
            // var plo = await _context.PLOs
            //     .Include(p => p.HocPhans.Where(h => h.SoTinChi > 0))
            //         .ThenInclude(h => h.LopHocPhans)
            //             .ThenInclude(l => l.SinhViens.Where(sv => sv.Id == sinhVienId))
            //     .Include(p => p.CLOs)
            //         .ThenInclude(c => c.CauHois)
            //             .ThenInclude(ch => ch.BaiKiemTra)
            //     .FirstOrDefaultAsync(p => p.Id == ploId) ?? throw new BusinessLogicException("Không tìm thấy PLO");
            var sinhVien = await _context.SinhViens
                    .AsSplitQuery()
                    .Include(sv => sv.Nganh)
                        .ThenInclude(n => n.PLOs.Where(pl => pl.Id == ploId))
                        .ThenInclude(pl => pl.HocPhans.Where(h => h.SoTinChi > 0))
                            .ThenInclude(h => h.LopHocPhans)
                    .FirstOrDefaultAsync(sv => sv.Id == sinhVienId) ?? throw new NotFoundException("Không tìm thấy sinh viên");

            var plo = sinhVien.Nganh.PLOs.FirstOrDefault(p => p.Id == ploId) ?? throw new NotFoundException("Không tìm thấy PLO");

            decimal totalPkMulSoTinChi = 0;
            decimal totalSoTinChi = 0;

            foreach (var hocPhan in plo.HocPhans)
            {
                decimal totalDiemCLO = 0;
                decimal totalMaxDiemCLO = 0;

                // For each CLO related to PLO
                // foreach (var clo in plo.CLOs)
                // {
                //     // Calculate diem-clo
                //     // decimal diemClo = 0;
                //     // var ketQuas = await _context.KetQuas
                //     //     .Where(kq => kq.SinhVienId == sinhVienId &&
                //     //                 clo.CauHois.Select(ch => ch.Id).Contains(kq.CauHoiId))
                //     //     .ToListAsync();

                //     // foreach (var ketQua in ketQuas)
                //     // {
                //     //     var cauHoi = clo.CauHois.First(ch => ch.Id == ketQua.CauHoiId);
                //     //     diemClo += ketQua.Diem * cauHoi.BaiKiemTra.TrongSo;
                //     // }
                //     var diemClo = await CalculateDiemCLO(sinhVienId, clo.Id, useDiemTam);
                //     totalDiemCLO += diemClo;

                //     // Calculate diem-clo-max
                //     decimal maxDiemClo = 0;
                //     // foreach (var cauHoi in clo.CauHois)
                //     // {
                //     //     maxDiemClo += 10m * cauHoi.TrongSo * cauHoi.BaiKiemTra.TrongSo ?? 0;
                //     // }
                //     maxDiemClo = await CalculateDiemCLOMax(clo.Id);
                //     totalMaxDiemCLO += maxDiemClo;
                // }
                // _logger.LogInformation("TotalDiemCLO: {TotalDiemCLO}, TotalMaxDiemCLO: {TotalMaxDiemCLO}, hocPhan.SoTinChi: {hocPhan.SoTinChi}", totalDiemCLO, totalMaxDiemCLO, hocPhan.SoTinChi);
                // totalPkMulSoTinChi = 10m * totalDiemCLO / totalMaxDiemCLO * hocPhan.SoTinChi;
                // totalSoTinChi += hocPhan.SoTinChi;

                decimal maxDiemPk = 0;
                foreach (var lopHocPhan in hocPhan.LopHocPhans)
                {
                    decimal diemPk = 0;
                    try {
                        diemPk = await CalculateDiemPk(lopHocPhan.Id, sinhVienId, ploId, useDiemTam);   
                    }
                    catch (Exception){
                        diemPk = 0;
                    }
                    maxDiemPk = Math.Max(maxDiemPk, diemPk);
                    _logger.LogInformation("MaxDiemPk: {MaxDiemPk}", maxDiemPk);    
                }

                _logger.LogInformation("hocPhan.SoTinChi: {hocPhan.SoTinChi}", hocPhan.SoTinChi);
                totalPkMulSoTinChi += maxDiemPk * hocPhan.SoTinChi;
                if (maxDiemPk != 0){
                    totalSoTinChi += hocPhan.SoTinChi;
                }
                _logger.LogInformation("TotalPkMulSoTinChi: {TotalPkMulSoTinChi}, TotalSoTinChi: {TotalSoTinChi}", totalPkMulSoTinChi, totalSoTinChi);
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