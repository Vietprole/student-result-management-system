using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.TaiKhoan;

public class UpdateTaiKhoanDTO
{
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int? ChucVuId { get; set; }
        public string? Ten { get; set; }
}