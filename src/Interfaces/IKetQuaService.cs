using Student_Result_Management_System.DTOs.KetQua;
using Student_Result_Management_System.Utils;

namespace Student_Result_Management_System.Interfaces
{
    public interface IKetQuaService
    {
        public Task<List<KetQuaDTO>> GetAllKetQuasAsync();
        public Task<List<KetQuaDTO>> GetFilteredKetQuasAsync(int? baiKiemTraId, int? sinhVienId, int? lopHocPhanId);
        // public Task<List<KetQuaDTO>> GetKetQuasByLopHocPhanIdAsync(int lopHocPhanId);
        public Task<KetQuaDTO?> GetKetQuaByIdAsync(int id);
        public Task<KetQuaDTO> CreateKetQuaAsync(CreateKetQuaDTO createKetQuaDTO);
        public Task<KetQuaDTO?> UpdateKetQuaAsync(int id, UpdateKetQuaDTO updateKetQuaDTO);
        public Task<KetQuaDTO> UpsertKetQuaAsync(UpdateKetQuaDTO ketQuaDTO);
        public Task<bool> DeleteKetQuaAsync(int id);
        public Task<KetQuaDTO?> ConfirmKetQuaAsync(ConfirmKetQuaDTO confirmKetQuaDTO);
        public Task<KetQuaDTO?> AcceptKetQuaAsync(AcceptKetQuaDTO acceptKetQuaDTO);
        public Task<decimal> CalculateDiemCLO(int sinhVienId, int cLOId, bool useDiemTam);
        public Task<decimal> CalculateDiemCLOMax(int cLOId);
        public Task<decimal> CalculateDiemPk(int lopHocPhanId, int sinhVienId, int ploId, bool useDiemTam);
        public Task<decimal> CalculateDiemPLO(int sinhVienId, int ploId, bool useDiemTam);
    }
}