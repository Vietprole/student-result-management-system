using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface IKhoaService
    {
        //public Task<bool> CheckTruongKhoa(string id); //Kiểm tra id đó có xuất hiện chưa (1 tài khoản - 1 trưởng khoa)
        public Task<List<Khoa>> GetListKhoa();
        //public Task<Khoa?> CreateKhoa(Khoa khoa);
        //public Task<int> CheckKhoa(string tenKhoa);
        public Task<string?> GetMaKhoa(int id);
        public Task<Khoa?> GetKhoaId(int id);
        public Task<Khoa?> UpdateTruongKhoa(int id, TaiKhoan truongkhoa);
        //public Task<Khoa?> GetKhoaByTruongKhoaId(string id);
    }
}