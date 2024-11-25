using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_Result_Management_System.DTOs.SinhVien;
using Student_Result_Management_System.DTOs.TaiKhoan;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface ISinhVienRepository
    {
        public Task<SinhVien?> CreateSinhVien(SinhVien sinhVien);
        public Task<SinhVien?> CheckSinhVien(CreateSinhVienDTO sinhVienDTO);
        public Task<int> GetSinhVienByKhoa(int khoaId);
    }
}