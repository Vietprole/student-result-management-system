using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_Result_Management_System.DTOs.CauHoi;

namespace Student_Result_Management_System.Interfaces
{
    public interface ICauHoiRepository
    {
        public Task<List<CauHoiDTO>> GetAllCauHoiByBaiKiemTraId(int baiKiemTraId);
        public Task<List<CauHoiDTO>> GetAllCauHoi();
        public Task<CauHoiDTO?> GetCauHoi(int id);
        public Task<CauHoiDTO> CreateCauHoi(CreateCauHoiDTO createCauHoiDTO);
        public Task<CauHoiDTO?> UpdateCauHoi(int id, UpdateCauHoiDTO updateCauHoiDTO);
        public Task<bool> DeleteCauHoi(int id);
    }
}