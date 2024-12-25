using Student_Result_Management_System.DTOs.BaiKiemTra;
using Student_Result_Management_System.DTOs.GiangVien;
using Student_Result_Management_System.DTOs.LopHocPhan;
using Student_Result_Management_System.DTOs.SinhVien;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface ILopHocPhanService
    {
        public Task<List<LopHocPhan>> GetAllLopHocPhansAsync();
        public Task<List<LopHocPhan>> GetFilteredLopHocPhansAsync(int? hocPhanId, int? hocKyId, int? giangVienId, int? sinhVienId);
        public Task<LopHocPhan?> GetLopHocPhanByIdAsync(int id);
        public Task<LopHocPhan?> CreateLopHocPhanAsync(CreateLopHocPhanDTO lopHocPhanDTO);
        public Task<LopHocPhan?> UpdateLopHocPhanAsync(int id,UpdateLopHocPhanDTO lopHocPhanDTO);
        public Task<bool> DeleteLopHocPhanAsync(int id);
        public Task<List<SinhVien>> GetSinhViensInLopHocPhanAsync(int lopHocPhanId);
        public Task<List<SinhVien>> AddSinhViensToLopHocPhanAsync(int lopHocPhanId, int[] sinhVienIds);
        public Task<List<SinhVien>> UpdateSinhViensInLopHocPhanAsync(int lopHocPhanId, int[] sinhVienIds);
        public Task<List<SinhVien>> RemoveSinhVienFromLopHocPhanAsync(int lopHocPhanId, int sinhVienId);
        public Task<string> CheckCongThucDiem(List<CreateBaiKiemTraDTO> createBaiKiemTraDTOs);
    }
}