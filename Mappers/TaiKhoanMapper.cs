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
        public static TaiKhoanDTO toTaiKhoanDTO(this TaiKhoan taiKhoan)
        {
            return new TaiKhoanDTO
            {
                TenDangNhap = taiKhoan.TenDangNhap,
                MatKhau = taiKhoan.MatKhau,
                ChucVuId = taiKhoan.ChucVuId,
                TrangThai = taiKhoan.TrangThai
            };
        }
        public static TaiKhoan toTaiKhoanFromTaiKhoanLoginDTO(this TaiKhoanLoginDTO taiKhoanLoginDTO)
        {
            return new TaiKhoan
            {
                TenDangNhap = taiKhoanLoginDTO.TenDangNhap,
                MatKhau = taiKhoanLoginDTO.MatKhau
            };
        }
        public static TaiKhoan ToTaiKhoanFromCreateDTO(this CreateTaiKhoanDTO taiKhoanDTO,int ChucVuId)
        {
            return new TaiKhoan
            {
                TenDangNhap = taiKhoanDTO.TenDangNhap,
                MatKhau = taiKhoanDTO.MatKhau,
                ChucVuId = ChucVuId,
                TrangThai = true
            };
        }
        
    }
}