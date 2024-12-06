using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_Result_Management_System.DTOs.KiHoc;

namespace Student_Result_Management_System.Interfaces
{
    public interface IKiHocRepository
    {
        public bool CheckTenKiHoc(string tenKiHoc);
        public bool CheckNamHoc(string namHoc);
        public Task<ViewKiHocDTO?> GetKiHocDTO(int id);
        public Task<List<ViewKiHocDTO>> GetAllKiHocDTO();
        public Task<ViewKiHocDTO> AddKiHocDTO(NewKiHocDTO newKiHocDTO);
        public Task<ViewKiHocDTO?> UpdateKiHocDTO(int id, NewKiHocDTO newKiHocDTO);
        public Task<bool> DuocSuaDiem(int id);
        public Task<bool> UpdateHanSuaDiem(int id, DateTime hanSuaDiem);
        public Task<bool> UpdateHanSuaCongThucDiem(int id, DateTime hanSuaCongThucDiem);
        public Task<bool> DeleteKiHocDTO(int id);
    }
}