using Student_Result_Management_System.DTOs.HocPhan;
using Student_Result_Management_System.DTOs.PLO;
using Student_Result_Management_System.Utils;

namespace Student_Result_Management_System.Interfaces
{
    public interface IHocPhanService
    {
        public Task<List<HocPhanDTO>> GetAllHocPhansAsync();
        public Task<List<HocPhanDTO>> GetHocPhansByKhoaIdAsync(int khoaId);
        public Task<HocPhanDTO?> GetHocPhanByIdAsync(int id);
        public Task<HocPhanDTO> CreateHocPhanAsync(CreateHocPhanDTO createHocPhanDTO);
        public Task<HocPhanDTO?> UpdateHocPhanAsync(int id, UpdateHocPhanDTO updateHocPhanDTO);
        public Task<bool> DeleteHocPhanAsync(int id);
        // public Task<ServiceResult<List<PLODTO>>> AddPLOsToHocPhanAsync(int hocPhanId, int[] pLOIds);
        public Task<ServiceResult<List<PLODTO>>> UpdatePLOsOfHocPhanAsync(int id, int[] pLOIds);
        // public Task<ServiceResult<List<PLODTO>>> RemovePLOsFromHocPhanAsync(int id, int[] pLOIds);
    }
}