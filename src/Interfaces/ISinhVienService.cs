using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_Result_Management_System.DTOs.SinhVien;
using Student_Result_Management_System.DTOs.TaiKhoan;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface ISinhVienService
    {
        public Task<List<SinhVien>> GetAll(int[] id);
        public Task<List<SinhVien>> GetAllSinhViens();
        public Task<List<SinhVien>> GetFilteredSinhViensAsync(int? khoaId, int? lopHocPhanId);
        public Task<SinhVienDTO?> CreateSinhVien(CreateSinhVienDTO sinhVienDTO);
        public Task<TaiKhoanDTO?> CreateTaiKhoanSinhVien(CreateSinhVienDTO taikhoanSinhVien);
        public Task<SinhVien?> CheckSinhVien(CreateSinhVienDTO sinhVienDTO);
        public Task<int> GetSinhVienByKhoa(int khoaId);
        public Task<SinhVien?> GetById(int id);
        public Task<SinhVien?> UpdateSV(int id,UpdateSinhVienDTO updateSinhVienDTO);
        public Task<SinhVien?> DeleteSV(int id);
    }
}