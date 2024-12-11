using Student_Result_Management_System.DTOs.KetQua;

namespace Student_Result_Management_System.Interfaces
{
    public interface IKetQuaService
    {
        public Task<List<KetQuaDTO>> GetAllKetQuasAsync();
        public Task<List<KetQuaDTO>> GetKetQuasByLopHocPhanIdAsync(int lopHocPhanId);
        public Task<KetQuaDTO?> GetKetQuaByIdAsync(int id);
        public Task<KetQuaDTO> CreateKetQuaAsync(CreateKetQuaDTO createKetQuaDTO);
        public Task<KetQuaDTO?> UpdateKetQuaAsync(int id, UpdateKetQuaDTO updateKetQuaDTO);
        public Task<bool> DeleteKetQuaAsync(int id);
    }
}