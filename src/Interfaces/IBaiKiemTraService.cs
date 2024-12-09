using Student_Result_Management_System.DTOs.BaiKiemTra;

namespace Student_Result_Management_System.Interfaces
{
    public interface IBaiKiemTraService
    {
        public Task<List<BaiKiemTraDTO>> GetAllBaiKiemTra();
        public Task<List<BaiKiemTraDTO>> GetAllBaiKiemTraByLopHocPhanId(int lopHocPhanId);
        public Task<BaiKiemTraDTO?> GetBaiKiemTra(int id);
        public Task<BaiKiemTraDTO> CreateBaiKiemTra(CreateBaiKiemTraDTO createBaiKiemTraDTO);
        public Task<BaiKiemTraDTO?> UpdateBaiKiemTra(int id, UpdateBaiKiemTraDTO updateBaiKiemTraDTO);
        public Task<bool> DeleteBaiKiemTra(int id);
    }
}