using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Result_Management_System.DTOs.TaiKhoan
{
    public class TaiKhoanDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Ten {  get; set; } = string.Empty;
        public string TenChucVu { get; set; } = string.Empty;
    }
}