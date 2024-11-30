using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Result_Management_System.DTOs.TaiKhoan
{
    public class TaiKhoanLoginDTO
    {
        [Required]
        public string TenDangNhap { get; set; } = string.Empty;
        [Required]
        public string MatKhau { get; set; } = string.Empty;
    }
}