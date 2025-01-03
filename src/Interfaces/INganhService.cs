using Student_Result_Management_System.DTOs.Nganh;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface INganhService
    {
        public Task<List<Nganh>> GetAllNganhsAsync();
        public Task<List<Nganh>> GetNganhsByKhoaIdAsync(int khoaId);
        public Task<Nganh?> GetNganhByIdAsync(int id);
        public Task<Nganh> CreateNganhAsync(Nganh nganh);
        public Task<Nganh?> UpdateNganhAsync(int id, UpdateNganhDTO updateNganhDTO);
        public Task<bool> DeleteNganhAsync(int id);
        public Task<List<HocPhan>> AddHocPhansToNganhAsync(int nganhId, int[] hocPhanIds);
        public Task<List<HocPhan>> UpdateHocPhansOfNganhAsync(int nganhId, int[] hocPhanIds);
        public Task<bool> RemoveHocPhanFromNganhAsync(int nganhId, int hocPhanId);
        public Task<List<HocPhan>> GetHocPhansInNganhAsync(int nganhId);
    }
}