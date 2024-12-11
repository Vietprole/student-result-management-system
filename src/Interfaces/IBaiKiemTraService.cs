using Student_Result_Management_System.DTOs.BaiKiemTra;

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
    }
}