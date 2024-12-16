using Student_Result_Management_System.DTOs.Khoa;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface IKhoaService
    {
        public Task<string> CheckCreateKhoa(CreateKhoaDTO createKhoaDTO);
        public Task<List<Khoa>> GetAllKhoasAsync();
        public Task<Khoa?> CreateKhoa(Khoa khoa);
        public Task<KhoaDTO?> UpdateKhoa(int id, UpdateKhoaDTO updateKhoaDTO);
        public Task<string?> GetMaKhoa(int id);
        public Task<Khoa?> GetKhoaByIdAsync(int id);
        public Task<Khoa?> UpdateTruongKhoa(int id, TaiKhoan truongkhoa);
    }
}