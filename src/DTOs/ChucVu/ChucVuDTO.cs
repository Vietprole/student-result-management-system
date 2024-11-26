using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Result_Management_System.DTOs.ChucVu
{
    public class ChucVuDTO
    {
        [Required]
        public string? TenChucVu { get; set; }
    }
}