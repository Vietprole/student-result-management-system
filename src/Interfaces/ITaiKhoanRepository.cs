using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_Result_Management_System.DTOs.ChucVu;
using Student_Result_Management_System.DTOs.TaiKhoan;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface ITaiKhoanRepository
    {
        public Task<TaiKhoan?> CreateUser(CreateTaiKhoanDTO createTaiKhoanDTO,ChucVuDTO chucVuDTO);
        public Task<TaiKhoan?> CreateTaiKhoanSinhVien(CreateTaiKhoanDTO taikhoanSinhVien);
        public Task<TaiKhoan?> CreateTaiKhoanGiangVien(CreateTaiKhoanDTO taikhoanGiangVien);
        public Task<TaiKhoan?> CheckUser(string username);
        public Task<bool> CheckPassword(TaiKhoan taikhoan, string password);
        public Task<TaiKhoan?> DeleteUser(TaiKhoan taikhoan);

    }
}