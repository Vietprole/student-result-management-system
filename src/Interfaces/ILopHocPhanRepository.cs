using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_Result_Management_System.DTOs.GiangVien;
using Student_Result_Management_System.DTOs.LopHocPhan;
using Student_Result_Management_System.DTOs.SinhVien;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface ILopHocPhanRepository
    {
        public Task<List<LopHocPhanDTO>> GetAllLopHocPhan();
        public Task<List<LopHocPhanDTO>> GetAllLopHocPhanByKiHocId(int kiHocId);
        public Task<List<LopHocPhanDTO>> GetAllLopHocPhanByHocPhanId(int hocPhanId);
        public Task<LopHocPhanDTO?> GetLopHocPhan(int id);
        public Task<LopHocPhanDTO> AddLopHocPhan(CreateLopHocPhanDTO lopHocPhanDTO);
        public Task<LopHocPhanDTO?> UpdateLopHocPhan(int id,UpdateLopHocPhanDTO lopHocPhanDTO);
        public Task<LopHocPhanDTO?> DeleteLopHocPhan(int id);
        public Task<List<SinhVienDTO>?> GetSinhVienDTOs(int lopHocPhanId);
        public Task<List<GiangVienDTO>?> GetGiangVienDTOs(int lopHocPhanId);
        public Task<List<SinhVienDTO>?> AddSinhViens(int lopHocPhanId, List<SinhVien> sinhViens);
        public Task<List<GiangVienDTO>?> AddGiangViens(int lopHocPhanId, List<GiangVien> giangViens);
        public Task<SinhVienDTO?> DeleteSinhViens(int lopHocPhanId, SinhVien sinhViens);
        public Task<GiangVienDTO?> DeleteGiangViens(int lopHocPhanId, GiangVien giangViens);
        public Task<DateTime?> CapNhatNgayXacNhanCTD(int lopHocPhanId, string tenNguoiXacNhanCTD);
        public Task<DateTime?> CapNhatNgayChapNhanCTD(int lopHocPhanId, string tenNguoiChapNhanCTD);
        
    }
}