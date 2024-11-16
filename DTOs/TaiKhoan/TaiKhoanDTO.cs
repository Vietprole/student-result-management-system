using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Result_Management_System.DTOs.TaiKhoan
{
    public class TaiKhoanDTO
    {
        public string TenDangNhap { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty;
        public int ChucVuId {get; set;}
        public Boolean TrangThai {get; set;} = true;

    }
}