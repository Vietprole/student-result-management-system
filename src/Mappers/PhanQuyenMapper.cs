using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_Result_Management_System.DTOs.PhanQuyen;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Mappers
{
    public static class PhanQuyenMapper
    {
        public static PhanQuyen ToPhanQuyenFromDTO(this PhanQuyenDTO dto)
        {
            return new PhanQuyen
            {
                Id = dto.Id,
                TenQuyen = dto.TenQuyen,
                ChucVuId = dto.ChucVuId
            };
        }
        public static PhanQuyenDTO ToPhanQuyenDTO(this PhanQuyen phanQuyen)
        {
            return new PhanQuyenDTO
            {
                Id = phanQuyen.Id,
                TenQuyen = phanQuyen.TenQuyen,
                ChucVuId = phanQuyen.ChucVuId
            };
        }
        public static PhanQuyen ToPhanQuyenFromCreateDTO(this CreatePhanQuyenDTO dto,int ChucVuId)
        {
            return new PhanQuyen
            {
                TenQuyen = dto.TenQuyen,
                ChucVuId = ChucVuId
            };
        }
        public static PhanQuyen ToPhanQuyenFromUpdateDTO(this UpdatePhanQuyenDTO dto)
        {
            return new PhanQuyen
            {
                TenQuyen = dto.TenQuyen,
                ChucVuId = dto.ChucVuId
            };
        }
    }
}