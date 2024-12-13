using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_Result_Management_System.DTOs.TaiKhoan;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface ITaiKhoanService
    {
        public Task<TaiKhoan?> GetTaiKhoanById(int id);
        public Task<NewTaiKhoanDTO?> CreateTaiKhoan(CreateTaiKhoanDTO username);
        public Task<TaiKhoanDTO?> CreateTaiKhoanSinhVien(CreateTaiKhoanDTO taikhoanSinhVien);
        public Task<string> CheckUsername(string username);
        public string CheckPassword(string password);
        public Task<NewTaiKhoanDTO?> Login(TaiKhoanLoginDTO taiKhoanLoginDTO);
        public Task<bool> DeleteTaiKhoan(int id);

    }
}