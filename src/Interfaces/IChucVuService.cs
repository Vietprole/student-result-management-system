using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface IChucVuService
    {
        public Task<ChucVu?> GetIdChucVuByTen(string tenChucVu);

        public Task<List<string>> GetListChucVu();
    }
}