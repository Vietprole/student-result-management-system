using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.BaiKiemTra;
using Student_Result_Management_System.DTOs.GiangVien;
using Student_Result_Management_System.DTOs.LopHocPhan;
using Student_Result_Management_System.DTOs.SinhVien;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Utils;

namespace Student_Result_Management_System.Services
{
    public class LopHocPhanService : ILopHocPhanService
    {
        private readonly ApplicationDBContext _context;
        public LopHocPhanService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<LopHocPhan>> GetAllLopHocPhansAsync()
        {
            var lopHocPhans = await _context.LopHocPhans
                .Include(lhp => lhp.HocPhan)
                .Include(lhp => lhp.HocKy)
                .Include(lhp => lhp.GiangVien)
                .ThenInclude(gv => gv!.TaiKhoan)
                .ToListAsync();
            return lopHocPhans;
        }

        public async Task<List<LopHocPhan>> GetFilteredLopHocPhansAsync(int? hocPhanId, int? hocKyId, int? giangVienId, int? sinhVienId)
        {
            IQueryable<LopHocPhan> query = _context.LopHocPhans
                .Include(lhp => lhp.HocPhan)
                .Include(lhp => lhp.HocKy)
                .Include(lhp => lhp.GiangVien)
                    .ThenInclude(gv => gv!.TaiKhoan)
                .Include(lhp => lhp.SinhViens);

            if (hocPhanId.HasValue)
            {
                query = query.Where(lhp => lhp.HocPhanId == hocPhanId.Value);
            }

            if (hocKyId.HasValue)
            {
                query = query.Where(lhp => lhp.HocKyId == hocKyId.Value);
            }

            if (giangVienId.HasValue)
            {
                query = query.Where(lhp => lhp.GiangVienId == giangVienId.Value);
            }

            if (sinhVienId.HasValue)
            {
                query = query.Where(lhp => lhp.SinhViens.Any(sv => sv.Id == sinhVienId.Value));
            }

            return await query.ToListAsync();
        }

        public async Task<LopHocPhan?> GetLopHocPhanByIdAsync(int id)
        {
            var lopHocPhan = await _context.LopHocPhans
                .Include(lhp => lhp.HocPhan)
                .Include(lhp => lhp.HocKy)
                .Include(lhp => lhp.GiangVien)
                .ThenInclude(gv => gv.TaiKhoan)
                .FirstOrDefaultAsync(s => s.Id == id);
            return lopHocPhan;
        }

        public async Task<LopHocPhan?> CreateLopHocPhanAsync(CreateLopHocPhanDTO lopHocPhanDTO)
        {
            var lopHocPhan = lopHocPhanDTO.ToLopHocPhanFromCreateDTO();
            var hocPhan = await _context.HocPhans.FindAsync(lopHocPhanDTO.HocPhanId) ?? throw new NotFoundException("Không tìm thấy Học phần");
            var hocKy = await _context.HocKies.FindAsync(lopHocPhanDTO.HocKyId) ?? throw new NotFoundException("Không tìm thấy Học kỳ");
            var maLopHocPhan = hocPhan.MaHocPhan + "." + hocKy.MaHocKy + "." + lopHocPhanDTO.Khoa + lopHocPhanDTO.Nhom;
            if (await _context.LopHocPhans.AnyAsync(lhp => lhp.MaLopHocPhan == maLopHocPhan))
            {
                throw new BusinessLogicException("Lớp học phần đã tồn tại");
            }
            lopHocPhan.MaLopHocPhan = maLopHocPhan;
            await _context.LopHocPhans.AddAsync(lopHocPhan);
            await _context.SaveChangesAsync();
            return GetLopHocPhanByIdAsync(lopHocPhan.Id).Result;
        }

        public async Task<LopHocPhan?> UpdateLopHocPhanAsync(int id, UpdateLopHocPhanDTO lopHocPhanDTO)
        {
            var lopHocPhan = await _context.LopHocPhans.Include(lhp => lhp.HocPhan).Include(lhp => lhp.HocKy).FirstOrDefaultAsync(lhp => lhp.Id == id) ?? throw new NotFoundException("Không tìm thấy Lớp học phần");
            lopHocPhanDTO.ToLopHocPhanFromUpdateDTO(lopHocPhan);
            await _context.SaveChangesAsync();
            return GetLopHocPhanByIdAsync(lopHocPhan.Id).Result;
        }

        public async Task<bool> DeleteLopHocPhanAsync(int id)
        {
            var lopHocPhan = await _context.LopHocPhans.FindAsync(id) ?? throw new NotFoundException("Không tìm thấy Lớp học phần");
            try
            {
                _context.LopHocPhans.Remove(lopHocPhan);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new BusinessLogicException("Lớp học phần chứa các đối tượng con, không thể xóa");
            }
            return true;
        }

        public async Task<List<SinhVienDTO>> GetSinhViensInLopHocPhanAsync(int lopHocPhanId)
        {
            var lopHocPhan = await _context.LopHocPhans
                .Include(c => c.SinhViens)
                    .ThenInclude(sv => sv.TaiKhoan)
                .Include(c => c.SinhViens)
                    .ThenInclude(sv => sv.Khoa) 
                .FirstOrDefaultAsync(s => s.Id == lopHocPhanId)
                ?? throw new NotFoundException("Không tìm thấy Lớp học phần");

            var sinhViens = lopHocPhan.SinhViens.ToList();
            return sinhViens.Select(sv => sv.ToSinhVienDTO()).ToList();
        }


        public async Task<List<SinhVien>> AddSinhViensToLopHocPhanAsync(int lopHocPhanId, int[] sinhVienIds)
        {
            var lopHocPhan = await _context.LopHocPhans
                .Include(lhp => lhp.SinhViens)
                .ThenInclude(t => t.TaiKhoan)
                .FirstOrDefaultAsync(lhp => lhp.Id == lopHocPhanId) ?? throw new NotFoundException("Không tìm thấy Lớp học phần");

            foreach (var sinhVienId in sinhVienIds)
            {
                bool isEnrolledInSameHocPhan = await _context.LopHocPhans
                    .Where(
                        lhp => (lhp.Id != lopHocPhanId) && 
                        (lhp.HocPhanId == lopHocPhan.HocPhanId) && 
                        (lhp.HocKyId == lopHocPhan.HocKyId)
                    ).AnyAsync(lhp => lhp.SinhViens.Any(sv => sv.Id == sinhVienId));

                if (isEnrolledInSameHocPhan)
                {
                    throw new BusinessLogicException($"Sinh viên với id {sinhVienId} đã được đăng ký cho học phần này trong học kỳ hiện tại");
                }

                var sinhVien = await _context.SinhViens.FindAsync(sinhVienId);
                if (sinhVien == null) continue;
                if (!lopHocPhan.SinhViens.Contains(sinhVien))
                {
                    lopHocPhan.SinhViens.Add(sinhVien);
                }
            }

            await _context.SaveChangesAsync();
            return [.. lopHocPhan.SinhViens];
        }

        public async Task<List<SinhVien>> UpdateSinhViensInLopHocPhanAsync(int lopHocPhanId, int[] sinhVienIds)
        {
            var lopHocPhan = await _context.LopHocPhans
                .Include(p => p.SinhViens)
                .ThenInclude(t => t.TaiKhoan)
                .FirstOrDefaultAsync(p => p.Id == lopHocPhanId) ?? throw new NotFoundException("Không tìm thấy lớp học phần");

            var sinhVienSet = new HashSet<int>(sinhVienIds);
            var sinhViens = await _context.SinhViens
                .Where(sv => sinhVienSet.Contains(sv.Id))
                .ToListAsync();

            lopHocPhan.SinhViens.Clear();
            lopHocPhan.SinhViens.AddRange(sinhViens);

            await _context.SaveChangesAsync();

            return [.. lopHocPhan.SinhViens];
        }

        public async Task<List<SinhVien>> RemoveSinhVienFromLopHocPhanAsync(int lopHocPhanId, int sinhVienId)
        {
            var lopHocPhan = await _context.LopHocPhans
                .Include(lhp => lhp.SinhViens)
                .FirstOrDefaultAsync(lhp => lhp.Id == lopHocPhanId) ?? throw new NotFoundException("Không tìm thấy lớp học phần");

            var sinhVien = await _context.SinhViens.FindAsync(sinhVienId) ?? throw new NotFoundException($"Không tìm thấy sinh viên có id: {sinhVienId}");
            if (!lopHocPhan.SinhViens.Contains(sinhVien)) throw new BusinessLogicException($"Sinh viên có id: {sinhVienId} không học trong lớp học phần này");

            lopHocPhan.SinhViens.Remove(sinhVien);
            await _context.SaveChangesAsync();
            return [.. lopHocPhan.SinhViens];
        }

        public Task<string> CheckCongThucDiem(List<CreateBaiKiemTraDTO> createBaiKiemTraDTOs)
        {
            bool ck = createBaiKiemTraDTOs.GroupBy(x => x.Loai).Any(g => g.Count() > 1);
            if (ck)
            {
                return Task.FromResult("Loại bài kiểm tra không được trùng nhau");
            }
            decimal sum = 0;
            foreach (CreateBaiKiemTraDTO i in createBaiKiemTraDTOs)
            {
                sum += i.TrongSo ?? 0;
            }
            if (sum != 1)
            {
                return Task.FromResult("Tổng trọng số phải bằng 1");
            }

            // Check date order
            foreach (var dto in createBaiKiemTraDTOs)
            {
                if (dto.NgayMoNhapDiem.HasValue && dto.HanNhapDiem.HasValue)
                {
                    if (dto.HanNhapDiem <= dto.NgayMoNhapDiem)
                    {
                        return Task.FromResult("Hạn nhập điểm phải sau ngày mở nhập điểm");
                    }
                }

                if (dto.HanNhapDiem.HasValue && dto.HanDinhChinh.HasValue)
                {
                    if (dto.HanDinhChinh <= dto.HanNhapDiem)
                    {
                        return Task.FromResult("Hạn định chính phải sau hạn nhập điểm");
                    }
                }
            }
            return Task.FromResult("OK");
        }

        public async Task<LopHocPhanChiTietDTO?> GetChiTietLopHocPhanDTO(int lopHocPhanId)
        {
            var lopHocPhan = await _context.LopHocPhans
                .Include(lhp => lhp.HocPhan)
                .Include(lhp => lhp.HocKy)
                .Include(lhp => lhp.GiangVien)
                .ThenInclude(gv => gv.TaiKhoan)
                .Include(lhp => lhp.SinhViens)
                .ThenInclude(sv => sv.TaiKhoan)
                .Include(lhp => lhp.BaiKiemTras)
                .FirstOrDefaultAsync(lhp => lhp.Id == lopHocPhanId);
            if (lopHocPhan == null)
            {
                return null;
            }
            var lopHocPhanChiTietDTO = new LopHocPhanChiTietDTO();
            lopHocPhanChiTietDTO.MaLopHocPhan = lopHocPhan.MaLopHocPhan;
            lopHocPhanChiTietDTO.TenLopHocPhan = lopHocPhan.Ten;
            lopHocPhanChiTietDTO.TenGiangVien = lopHocPhan.GiangVien?.TaiKhoan?.Ten ?? "N/A";
            lopHocPhanChiTietDTO.SoLuongSinhVien = lopHocPhan.SinhViens.Count;
            lopHocPhanChiTietDTO.NamHoc = lopHocPhan.HocKy.NamHoc;
            lopHocPhanChiTietDTO.TenHocKy = lopHocPhan.HocKy.Ten;
            return lopHocPhanChiTietDTO;
        }

        public async Task<List<SinhVienDTO>> GetSinhViensNotInLopHocPhanDTO(int lopHocPhanId)
        {
            var sinhViensInLopHocPhan = await _context.LopHocPhans
                .Where(lhp => lhp.Id == lopHocPhanId)
                .SelectMany(lhp => lhp.SinhViens)
                .ToListAsync();
            var sinhViensNotInLopHocPhan = await _context.SinhViens
                .Include(c=> c.TaiKhoan)
                .Include(c => c.Khoa)
                .Where(sv => !sinhViensInLopHocPhan
                .Select(s => s.Id)
                .Contains(sv.Id))
                .ToListAsync();
            return sinhViensNotInLopHocPhan.Select(sv => sv.ToSinhVienDTO()).ToList();

        }

        //public async Task<DateTime?> CapNhatNgayChapNhanCTD(int lopHocPhanId, string tenNguoiChapNhanCTD)
        //{
        //    var lopHocPhan =await _context.LopHocPhans.FirstOrDefaultAsync(s => s.Id == lopHocPhanId);
        //    if (lopHocPhan == null)
        //    {
        //        return null;
        //    }
        //    lopHocPhan.TenNguoiChapNhanCTD = tenNguoiChapNhanCTD;
        //    lopHocPhan.NgayChapNhanCTD = DateTime.Now.Date;
        //    await _context.SaveChangesAsync();
        //    return lopHocPhan.NgayChapNhanCTD;

        //}

        //public async Task<DateTime?> CapNhatNgayXacNhanCTD(int lopHocPhanId, string tenNguoiXacNhanCTD)
        //{
        //    var lopHocPhan = await _context.LopHocPhans.FirstOrDefaultAsync(s => s.Id == lopHocPhanId);
        //    if (lopHocPhan == null)
        //    {
        //        return null;
        //    }
        //    lopHocPhan.TenNguoiXacNhanCTD = tenNguoiXacNhanCTD;
        //    lopHocPhan.NgayXacNhanCTD = DateTime.Now.Date;
        //    await _context.SaveChangesAsync();
        //    return lopHocPhan.NgayXacNhanCTD;
        //}
    }
}