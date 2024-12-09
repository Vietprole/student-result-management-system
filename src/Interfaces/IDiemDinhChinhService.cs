using Student_Result_Management_System.DTOs.DiemDinhChinh; 

namespace StudentResultManagementSystem.Interfaces
{
    public interface IDiemDinhChinhService
    {
        public Task<List<DiemDinhChinhDTO>> GetDiemDinhChinhsAsync();
        public Task<DiemDinhChinhDTO?> GetDiemDinhChinhByIdAsync(int id);
        public Task<DiemDinhChinhDTO> CreateDiemDinhChinhAsync(CreateDiemDinhChinhDTO createDiemDinhChinhDTO);
        public Task<DiemDinhChinhDTO?> UpdateDiemDinhChinhAsync(int id, UpdateDiemDinhChinhDTO updateDiemDinhChinhDTO);
        public Task<bool> DeleteDiemDinhChinhAsync(int id);
    }
}