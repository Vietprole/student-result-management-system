using Student_Result_Management_System.DTOs.CLO;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface ICLOService
    {
        public Task<List<CLO>> GetFilteredCLOsAsync(int? hocPhanId, int? hocKyId);
        public Task<List<CLODTO>> GetCLOsByLopHocPhanIdAsync(int lopHocPhanId);
        public Task<List<CLODTO>> GetCLOsByCauHoiIdAsync(int cauHoiId);
        public Task<List<CLODTO>> GetCLOsByPLOIdAsync(int ploId);
        public Task<CLODTO?> GetCLOByIdAsync(int id);
        public Task<CLODTO> CreateCLOAsync(CreateCLODTO createCLODTO);
        public Task<CLODTO?> UpdateCLOAsync(int id, UpdateCLODTO updateCLODTO);
        public Task<bool> DeleteCLOAsync(int id);
    }
}