using Student_Result_Management_System.DTOs.Nganh;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface INganhService
    {
        // public Task<List<Nganh>> GetAllNganhsAsync();
        // public Task<List<Nganh>> GetNganhsByKhoaIdAsync(int khoaId);
        public Task<Nganh?> GetNganhByIdAsync(int id);
        // public Task<Nganh> CreateNganhAsync(CreateNganhDTO createNganhDTO);
        // public Task<Nganh?> UpdateNganhAsync(int id, UpdateNganhDTO updateNganhDTO);
        public Task<bool> DeleteNganhAsync(int id);
    }
}