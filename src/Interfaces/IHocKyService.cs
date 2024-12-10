using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_Result_Management_System.DTOs.HocKy;

namespace Student_Result_Management_System.Interfaces
{
    public interface IHocKyService
    {
        public bool CheckTenHocKy(string tenHocKy);
        public bool CheckNamHoc(string namHoc);
        public Task<HocKyDTO?> GetHocKyDTO(int id);
        public Task<List<HocKyDTO>> GetAllHocKyDTO();
        public Task<HocKyDTO> AddHocKyDTO(CreateHocKyDTO newHocKyDTO);
        public Task<HocKyDTO?> UpdateHocKyDTO(int id, CreateHocKyDTO newHocKyDTO);
        public Task<bool> UpdateHanSuaDiem(int id, DateTime hanSuaDiem);
        public Task<bool> UpdateHanSuaCongThucDiem(int id, DateTime hanSuaCongThucDiem);
        public Task<bool> DeleteHocKyDTO(int id);
    }
}