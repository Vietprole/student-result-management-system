using Student_Result_Management_System.DTOs.CLO; 

namespace StudentResultManagementSystem.Interfaces
{
    public interface ICLOService
    {
        public Task<List<CLODTO>> GetCLOsAsync(int? lopHocPhanId);
        public Task<CLODTO?> GetCLOByIdAsync(int id);
        public Task<CLODTO> CreateCLOAsync(CreateCLODTO createCLODTO);
        public Task<CLODTO?> UpdateCLOAsync(int id, UpdateCLODTO updateCLODTO);
        public Task<bool> DeleteCLOAsync(int id);
    }
}