using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_Result_Management_System.DTOs.GiangVien;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface IGiangVienRepository
    {
        public Task<List<GiangVien>> GetAll(int[] id);
        public Task<List<GiangVien>> GetAllByKhoaId(int khoaId);
        public Task<List<GiangVien>> GetAllGiangVien();
        public Task<string> GetKhoaId(string taikhoanId);
        public Task<GiangVien?> GetById(int id);
        public Task<TaiKhoan?> CreateTaiKhoanGiangVien(CreateGiangVienDTO createGiangVienDTO);
        public Task<GiangVien?> CreateGiangVien(GiangVien giangVien,TaiKhoan taiKhoan);
        public Task<GiangVien?> CheckGiangVien(CreateGiangVienDTO giangVienDTO);
        public Task<int> GetCountGiangVien(int khoaId);
        public Task<GiangVien?> UpdateGV(int id,UpdateGiangVienDTO updateGiangVienDTO);
        public Task<GiangVien?> DeleteGV(int id);
    }
}