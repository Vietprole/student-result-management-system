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
        public Task<GiangVien?> CreateGiangVien(GiangVien giangVien);
        public Task<GiangVien?> CheckGiangVien(CreateGiangVienDTO giangVienDTO);
        public Task<int> GetCountGiangVien(int khoaId);
    }
}