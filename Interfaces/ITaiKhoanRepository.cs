using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_Result_Management_System.DTOs.TaiKhoan;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface ITaiKhoanRepository
    {
        Task<TaiKhoanDTO?> CheckLogin(string tenDangNhap, string matKhau);
        Task<TaiKhoan> CreateTaiKhoan(TaiKhoan taiKhoan);
        Task<bool> TaiKhoanExists(int id);
        Task<bool> TenDangNhapExists(string tenDangNhap);
        Task<bool> UpdateMatKhau(int id, string matKhau);
        Task<TaiKhoan?> DeleteTaiKhoan(int id);
    }
}