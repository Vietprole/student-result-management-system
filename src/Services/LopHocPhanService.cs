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

namespace Student_Result_Management_System.Services
{
    public class LopHocPhanService : ILopHocPhanService
    {
        private readonly ApplicationDBContext _context;
        public LopHocPhanService(ApplicationDBContext context)
        {
            _context = context;
        }
        //public async Task<List<GiangVienDTO>?> AddGiangViens(int lopHocPhanId, List<GiangVien> giangViens)
        //{
        //    var lopHocPhan = await _context.LopHocPhans.Include(c=>c.GiangViens).ThenInclude(x=>x.TaiKhoan).FirstOrDefaultAsync(s => s.Id == lopHocPhanId);
        //    if (lopHocPhan == null)
        //    {
        //        return null;
        //    }
        //    foreach (var giangVien in giangViens)
        //    {
        //        // Kiểm tra nếu giảng viên chưa tồn tại trong danh sách
        //        if (!lopHocPhan.GiangViens.Any(g => g.Id == giangVien.Id))
        //        {
        //            lopHocPhan.GiangViens.Add(giangVien);
        //        }
        //    }
        //    await _context.SaveChangesAsync();
        //    var giangVienDTOs = giangViens.Select(s => s.ToGiangVienDTO()).ToList();
        //    return giangVienDTOs;
        //}

        public async Task<LopHocPhanDTO> AddLopHocPhan(CreateLopHocPhanDTO lopHocPhanDTO)
        {
            var lopHocPhan = lopHocPhanDTO.ToLopHocPhanFromCreateDTO();
            await _context.LopHocPhans.AddAsync(lopHocPhan);
            await _context.SaveChangesAsync();
            return lopHocPhan.ToLopHocPhanDTO();
        }

        public async Task<List<SinhVienDTO>?> AddSinhViens(int lopHocPhanId, List<SinhVien> sinhViens)
        {
            var lopHocPhan = await _context.LopHocPhans.Include(c => c.SinhViens).ThenInclude(x => x.TaiKhoan).FirstOrDefaultAsync(s => s.Id == lopHocPhanId);
            if (lopHocPhan == null)
            {
                return null;
            }
            foreach (var sinhVien in sinhViens)
            {
                if (!lopHocPhan.SinhViens.Any(s => s.Id == sinhVien.Id))
                {
                    lopHocPhan.SinhViens.Add(sinhVien);
                }
            }
            await _context.SaveChangesAsync();
            var sinhVienDTOs = sinhViens.Select(s => s.ToSinhVienDTO()).ToList();
            return sinhVienDTOs;
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

        //public async Task<GiangVienDTO?> DeleteGiangViens(int lopHocPhanId, GiangVien giangViens)
        //{
        //    var lopHocPhan = await _context.LopHocPhans.Include(c => c.GiangViens).ThenInclude(x => x.TaiKhoan).FirstOrDefaultAsync(s => s.Id == lopHocPhanId);
        //    if (lopHocPhan == null)
        //    {
        //        return null;
        //    }
        //    if (!lopHocPhan.GiangViens.Any(s => s.Id == giangViens.Id)) //Kiểm tra xem giảng viên có trong danh sách không
        //    {
        //        return null;
        //    }
        //    lopHocPhan.GiangViens.Remove(giangViens);
        //    await _context.SaveChangesAsync();
        //    return giangViens.ToGiangVienDTO();
        //}

        public async Task<LopHocPhanDTO?> DeleteLopHocPhan(int id)
        {
            var lopHocPhan = await _context.LopHocPhans.FindAsync(id);
            if (lopHocPhan == null)
            {
                return null;
            }
            _context.LopHocPhans.Remove(lopHocPhan);
            await _context.SaveChangesAsync();
            return lopHocPhan.ToLopHocPhanDTO();
        }

        public async Task<SinhVienDTO?> DeleteSinhViens(int lopHocPhanId, SinhVien sinhViens)
        {
            var lopHocPhan = await _context.LopHocPhans.Include(c => c.SinhViens).ThenInclude(x => x.TaiKhoan).FirstOrDefaultAsync(s => s.Id == lopHocPhanId);
            if (lopHocPhan == null)
            {
                return null;
            }
            if (!lopHocPhan.SinhViens.Any(s => s.Id == sinhViens.Id))
            {
                return null;
            }
            lopHocPhan.SinhViens.Remove(sinhViens);
            await _context.SaveChangesAsync();
            return sinhViens.ToSinhVienDTO();
        }

        public async Task<List<LopHocPhanDTO>> GetAllLopHocPhan()
        {
            var lopHocPhans =await _context.LopHocPhans.ToListAsync();
            var lopHocPhanDTOs=lopHocPhans.Select(s=>s.ToLopHocPhanDTO()).ToList();
            return lopHocPhanDTOs;
        }

        public async Task<List<LopHocPhanDTO>> GetAllLopHocPhanByHocPhanId(int hocPhanId)
        {
            var lopHocPhan = await _context.LopHocPhans.Where(s => s.HocPhanId == hocPhanId).ToListAsync();
            var lopHocPhanDTOs = lopHocPhan.Select(s => s.ToLopHocPhanDTO()).ToList();
            return lopHocPhanDTOs;
        }

        public async Task<List<LopHocPhanDTO>> GetAllLopHocPhanByHocKyId(int hocKyId)
        {
            var lopHocPhan = await _context.LopHocPhans.Where(s => s.HocKyId == hocKyId).ToListAsync();
            var lopHocPhanDTOs = lopHocPhan.Select(s => s.ToLopHocPhanDTO()).ToList();
            return lopHocPhanDTOs; 
        }

        //public async Task<List<GiangVienDTO>?> GetGiangVienDTOs(int lopHocPhanId)
        //{
        //    var lopHocPhan = await _context.LopHocPhans.Include(c => c.GiangViens).ThenInclude(x => x.TaiKhoan).FirstOrDefaultAsync(s => s.Id == lopHocPhanId);
        //    if (lopHocPhan == null)
        //    {
        //        return null;
        //    }
        //    var giangVienDTOs = lopHocPhan.GiangViens.Select(s => s.ToGiangVienDTO()).ToList();
        //    return giangVienDTOs;
        //}

        public async Task<LopHocPhanDTO?> GetLopHocPhan(int id)
        {
            var lopHocPhan =await _context.LopHocPhans.FindAsync(id);
            if (lopHocPhan == null)
            {
                return null;
            }
            return lopHocPhan.ToLopHocPhanDTO();
        }

        public async Task<List<SinhVienDTO>?> GetSinhVienDTOs(int lopHocPhanId)
        {
            var lopHocPhan = await _context.LopHocPhans.Include(c => c.SinhViens).ThenInclude(x => x.TaiKhoan).FirstOrDefaultAsync(s => s.Id == lopHocPhanId);
            if (lopHocPhan == null)
            {
                return null;
            }
            var sinhVienDTOs = lopHocPhan.SinhViens.Select(s => s.ToSinhVienDTO()).ToList();
            return sinhVienDTOs;
        }

        public async Task<LopHocPhanDTO?> UpdateLopHocPhan(int id,UpdateLopHocPhanDTO lopHocPhanDTO)
        {
            var lopHocPhan = await _context.LopHocPhans.FindAsync(id);
            if (lopHocPhan == null)
            {
                return null;
            }
            lopHocPhan.Ten = lopHocPhanDTO.Ten;
            lopHocPhan.HocPhanId = lopHocPhanDTO.HocPhanId;
            lopHocPhan.HocKyId = lopHocPhanDTO.HocKyId;
            await _context.SaveChangesAsync();
            return lopHocPhan.ToLopHocPhanDTO();
        }

        public Task<string> CheckCongThucDiem(List<CreateBaiKiemTraDTO> createBaiKiemTraDTOs)
        {
            decimal sum = 0;
            foreach(CreateBaiKiemTraDTO i in createBaiKiemTraDTOs)
            {
                sum += i.TrongSo ?? 0;
            }
            if(sum!=1)
            {
                return Task.FromResult("Tổng trọng số phải bằng 1");
            }
            return Task.FromResult("OK");
        }
    }
}