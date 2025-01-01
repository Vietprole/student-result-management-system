using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_Result_Management_System.DTOs.TaiKhoan;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Mappers
{
    public static class TaiKhoanMapper
    {
        public static TaiKhoan ToTaiKhoanFromCreateTaiKhoanDTO(this CreateTaiKhoanDTO createTaiKhoanDTO)
        {
            return new TaiKhoan
            {
                Username = createTaiKhoanDTO.Username,
                Ten = createTaiKhoanDTO.Ten,
                Password = createTaiKhoanDTO.Password
               
            };
        }
        public static TaiKhoanProfileDTO ToTaiKhoanProfileDTO(this TaiKhoan taiKhoan, string chucVu)
        {
            return new TaiKhoanProfileDTO
            {
                Ten = taiKhoan.Ten,
                ChucVu = chucVu 
            };
        }
        public static TaiKhoanDTO ToTaiKhoanDTO(this TaiKhoan taiKhoan)
        {
            return new TaiKhoanDTO
            {
                Id = taiKhoan.Id,
                Username = taiKhoan.Username,
                Ten = taiKhoan.Ten,
                ChucVuId = taiKhoan.ChucVuId,
                TenChucVu = taiKhoan.ChucVu.TenChucVu
            };
        }

        public static TaiKhoan ToTaiKhoanFromUpdateDTO(this UpdateTaiKhoanDTO updateTaiKhoanDTO, TaiKhoan existingTaiKhoan)
        {
            existingTaiKhoan.Username = updateTaiKhoanDTO.Username ?? existingTaiKhoan.Username;
            existingTaiKhoan.Ten = updateTaiKhoanDTO.Ten ?? existingTaiKhoan.Ten;
            existingTaiKhoan.ChucVuId = updateTaiKhoanDTO.ChucVuId ?? existingTaiKhoan.ChucVuId;
            return existingTaiKhoan;
        }

    }
}