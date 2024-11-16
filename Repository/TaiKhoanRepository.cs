using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.DTOs.TaiKhoan;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Mappers;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Repository
{
    public class TaiKhoanRepository : ITaiKhoanRepository
    {
        private readonly ApplicationDBContext  _context;
        public TaiKhoanRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<TaiKhoanDTO?> CheckLogin(string tenDangNhap, string matKhau)
        {
            var taikhoan = await _context.TaiKhoans.FirstOrDefaultAsync(x => x.TenDangNhap == tenDangNhap && x.MatKhau == matKhau);
            if (taikhoan == null)
            {
                return null;
            }
            return taikhoan.toTaiKhoanDTO();
        }

        public async Task<TaiKhoan> CreateTaiKhoan(TaiKhoan taiKhoan)
        {
            await _context.TaiKhoans.AddAsync(taiKhoan);
            await _context.SaveChangesAsync();
            return taiKhoan;
        }

        public async Task<TaiKhoan?> DeleteTaiKhoan(int id)
        {
            var taikhoanModel = await _context.TaiKhoans.FirstOrDefaultAsync(x => x.Id == id);
            if (taikhoanModel == null)
            {
                return null;
            }
            _context.TaiKhoans.Remove(taikhoanModel);
            _context.SaveChanges();
            return taikhoanModel;
        }

        public async Task<bool> TaiKhoanExists(int id)
        {
            return await _context.TaiKhoans.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> TenDangNhapExists(string tenDangNhap)
        {
            return await _context.TaiKhoans.AnyAsync(x => x.TenDangNhap == tenDangNhap);
        }

        public async Task<bool> UpdateMatKhau(int id, string matKhau)
        {
            var taikhoanModel = await _context.TaiKhoans.FirstOrDefaultAsync(x => x.Id == id);
            if (taikhoanModel == null)
            {
                return false;
            }
            taikhoanModel.MatKhau = matKhau;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}