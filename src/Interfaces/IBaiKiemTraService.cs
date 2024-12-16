using Student_Result_Management_System.DTOs.BaiKiemTra;
using Student_Result_Management_System.DTOs.LopHocPhan;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface IBaiKiemTraService
    {
        public Task<List<BaiKiemTraDTO>> GetAllBaiKiemTrasAsync();
        public Task<List<BaiKiemTraDTO>> GetBaiKiemTrasByLopHocPhanIdAsync(int lopHocPhanId);
        public Task<BaiKiemTraDTO?> GetBaiKiemTraByIdAsync(int id);
        public Task<BaiKiemTraDTO> CreateBaiKiemTraAsync(CreateBaiKiemTraDTO createBaiKiemTraDTO);
        public Task<BaiKiemTraDTO?> UpdateBaiKiemTraAsync(int id, UpdateBaiKiemTraDTO updateBaiKiemTraDTO);
        public Task<bool> DeleteBaiKiemTraAsync(int id);
        public Task<bool> CheckDuplicateBaiKiemTraLoaiInLopHocPhan(string? loai, int lopHocPhanId);
        public Task<CongThucDiemDTO?> CreateCongThucDiem(int lopHocPhanId,List<CreateBaiKiemTraDTO> createBaiKiemTraDTOs);
    }
}