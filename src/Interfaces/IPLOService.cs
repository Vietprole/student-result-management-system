using Student_Result_Management_System.DTOs.CLO;
using Student_Result_Management_System.DTOs.HocPhan;
using Student_Result_Management_System.DTOs.PLO;
using Student_Result_Management_System.Models;
using Student_Result_Management_System.Utils;

namespace Student_Result_Management_System.Interfaces
{
    public interface IPLOService
    {
        public Task<List<PLODTO>> GetAllPLOsAsync();
        public Task<List<PLODTO>> GetPLOsByNganhIdAsync(int nganhId);
        public Task<List<PLODTO>> GetPLOsByLopHocPhanIdAsync(int lopHocPhanId);
        public Task<List<PLODTO>> GetPLOsByHocPhanIdAsync(int hocPhanId);
        public Task<PLODTO?> GetPLOByIdAsync(int id);
        public Task<PLODTO> CreatePLOAsync(CreatePLODTO createPLODTO);
        public Task<PLODTO?> UpdatePLOAsync(int id, UpdatePLODTO updatePLODTO);
        public Task<bool> DeletePLOAsync(int id);
        public Task<List<CLODTO>> UpdateCLOsOfPLOAsync(int ploId, int[] cLOIds);
        public Task<List<HocPhanDTO>> UpdateHocPhansOfPLOAsync(int ploId, int[] hocPhanIds);
    }
}