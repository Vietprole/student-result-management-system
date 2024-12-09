using Student_Result_Management_System.DTOs.ChucVu;
using Student_Result_Management_System.DTOs.TaiKhoan;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface ITaiKhoanService
    {
        public Task<TaiKhoan?> GetById(string id);
        public Task<List<string>> GetRoles(TaiKhoan taikhoan);
        public Task<TaiKhoan?> CreateUser(CreateTaiKhoanDTO createTaiKhoanDTO,ChucVuDTO chucVuDTO);
        public Task<TaiKhoan?> CreateTaiKhoanSinhVien(CreateTaiKhoanDTO taikhoanSinhVien);
        public Task<TaiKhoan?> CreateTaiKhoanGiangVien(CreateTaiKhoanDTO taikhoanGiangVien);
        public Task<TaiKhoan?> CheckUser(string username);
        public Task<bool> CheckPassword(TaiKhoan taikhoan, string password);
        public Task<TaiKhoan?> DeleteUser(TaiKhoan taikhoan);

    }
}