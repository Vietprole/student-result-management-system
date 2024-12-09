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
                Ten = createTaiKhoanDTO.HovaTen
            };
        }
        public static TaiKhoanProfileDTO ToTaiKhoanProfileDTO(this TaiKhoan taiKhoan, string chucVu)
        {
            return new TaiKhoanProfileDTO
            {
                HovaTen = taiKhoan.Ten,
                ChucVu = chucVu 
            };
        }

    }
}