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
        public List<TaiKhoanDTO> GetFilteredTaiKhoans(int? chucVuId);
        public TaiKhoan? GetTaiKhoanById(int id);
        public Task<NewTaiKhoanDTO?> CreateTaiKhoan(CreateTaiKhoanDTO username);
        public TaiKhoanDTO? CreateTaiKhoanSinhVien(CreateTaiKhoanDTO taikhoanSinhVien);
        public TaiKhoanDTO? UpdateTaiKhoan(int id, UpdateTaiKhoanDTO updateTaiKhoanDTO);
        public string CheckUsername(string username);
        public string CheckPassword(string password);
        public Task<NewTaiKhoanDTO?> Login(TaiKhoanLoginDTO taiKhoanLoginDTO);
        public bool DeleteTaiKhoan(int id);
        public bool ChangePassword(int id, ChangePasswordDTO changePasswordDTO);
        public bool ResetPassword(int id);
        public bool ResetPasswordForSinhVienGiangVien(int id);
    }
}