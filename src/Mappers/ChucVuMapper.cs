using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_Result_Management_System.DTOs.ChucVu;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Mappers
{
    public static class ChucVuMapper
    {
        public static ChucVuDTO toChucVuDTO(this ChucVu chucVu)
        {
            return new ChucVuDTO
            {
                Id = chucVu.Id,
                TenChucVu = chucVu.TenChucVu
            };
        }
        public static ChucVu toChucVu(this ChucVuDTO chucVuDTO)
        {
            return new ChucVu
            {
                Id = chucVuDTO.Id,
                TenChucVu = chucVuDTO.TenChucVu
            };
        }
        public static ChucVu ToChucVuFromCreateChucVuDTO(this CreateChucVuDTO createChucVuDTO)
        {
            return new ChucVu
            {
                TenChucVu = createChucVuDTO.TenChucVu
            };
        }
        public static ChucVu ToChucVuFromUpdateChucVuDTO(this UpdateChucVuDTO updateChucVuDTO)
        {
            return new ChucVu
            {
                TenChucVu = updateChucVuDTO.TenChucVu
            };
        }
    }
}