using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_Result_Management_System.DTOs.ChucVu;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface IChucVuRepository
    { 
        Task<List<ChucVu>> GetAllChucVu();
        Task<ChucVu?> GetChucVuByTenChucVu(string tenchucvu);
        Task<ChucVu?> GetChucVuById(int id);
        Task<ChucVu> CreateChucVu(ChucVu chucVu);
        Task<ChucVu?> UpdateChucVu(int id,ChucVu chucVu);
        Task<ChucVu?> DeleteChucVu(int id);
        Task<bool> ChucVuExists(int id);
    }
}