using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Result_Management_System.DTOs.HocKy
{
    public class CreateHocKyDTO
    {
        [Required(ErrorMessage = "Tên học kỳ không được để trống")]
        public string Ten { get; set; } = string.Empty;

        [Required]
        [Range(1900, 9999, ErrorMessage = "Năm học phải từ 1900 đến 9999")]
        public int NamHoc { get; set; }

        [Required(ErrorMessage = "Mã học kỳ không được để trống")]
        public string MaHocKy { get; set; } = string.Empty;
    }
}