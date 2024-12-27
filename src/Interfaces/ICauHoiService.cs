using Student_Result_Management_System.DTOs.CauHoi;
using Student_Result_Management_System.DTOs.CLO;
using Student_Result_Management_System.Utils;

namespace Student_Result_Management_System.Interfaces
{
    public interface ICauHoiService
    {
        public Task<List<CauHoiDTO>> GetAllCauHoisAsync();
        public Task<List<CauHoiDTO>> GetCauHoisByBaiKiemTraIdAsync(int baiKiemTraId);
        public Task<CauHoiDTO?> GetCauHoiByIdAsync(int id);
        public Task<CauHoiDTO> CreateCauHoiAsync(CreateCauHoiDTO createCauHoiDTO);
        public Task<CauHoiDTO?> UpdateCauHoiAsync(int id, UpdateCauHoiDTO updateCauHoiDTO);
        public Task<bool> DeleteCauHoiAsync(int id);
        // public Task<<List<CLODTO>> AddCLOsToCauHoiAsync(int cauHoiId, int[] cLOIds);
        public Task<List<CLODTO>> UpdateCLOsOfCauHoiAsync(int id, int[] cLOIds);
        // public Task<List<CLODTO>> RemoveCLOsFromCauHoiAsync(int id, int[] cLOIds);
        public Task<List<CauHoiDTO>> UpdateListCauHoiAsync(int id, List<CreateCauHoiDTO> createCauHoiDTOs);
    }
}