using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_Result_Management_System.DTOs.BaiKiemTra;

namespace Student_Result_Management_System.Interfaces
{
    public interface IBaiKiemTraRepository
    {
        public Task<List<BaiKiemTraDTO>> GetAllBaiKiemTra();
        public Task<List<BaiKiemTraDTO>> GetAllBaiKiemTraByLopHocPhanId(int lopHocPhanId);
        public Task<BaiKiemTraDTO?> GetBaiKiemTra(int id);
        public Task<BaiKiemTraDTO> CreateBaiKiemTra(CreateBaiKiemTraDTO createBaiKiemTraDTO);
        public Task<BaiKiemTraDTO?> UpdateBaiKiemTra(int id, UpdateBaiKiemTraDTO updateBaiKiemTraDTO);
        public Task<bool> DeleteBaiKiemTra(int id);
    }
}