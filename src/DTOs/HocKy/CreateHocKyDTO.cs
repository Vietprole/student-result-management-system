using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Result_Management_System.DTOs.HocKy
{
    public class CreateHocKyDTO
    {
        [Required]
        [MinLength(1)]
        public string Ten { get; set; } = string.Empty;
        [Required]
        [Range(0, 9999)]
        public int NamHoc { get; set; }
        [Required]
        [MinLength(1)]
        public string MaHocKy { get; set; } = string.Empty;
    }
}