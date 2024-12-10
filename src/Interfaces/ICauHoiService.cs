using Student_Result_Management_System.DTOs.CauHoi;

namespace Student_Result_Management_System.Interfaces
{
    public interface ICauHoiService
    {
        public Task<List<CauHoiDTO>> GetCauHoisByBaiKiemTraIdAsync(int baiKiemTraId);
        public Task<List<CauHoiDTO>> GetAllCauHoisAsync();
        public Task<CauHoiDTO?> GetCauHoiByIdAsync(int id);
        public Task<CauHoiDTO> CreateCauHoiAsync(CreateCauHoiDTO createCauHoiDTO);
        public Task<CauHoiDTO?> UpdateCauHoiAsync(int id, UpdateCauHoiDTO updateCauHoiDTO);
        public Task<bool> DeleteCauHoiAsync(int id);
        public Task<bool> AddCLOsToCauHoiAsync(int cauHoiId, int[] cLOIds);
    }
}