using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Result_Management_System.DTOs.TaiKhoan
{
    public class CreateTaiKhoanDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public int ChucVuId { get; set; }
        [Required]
        public string HovaTen { get; set; } = string.Empty;
        public int? KhoaId { get; set; }
    }
}