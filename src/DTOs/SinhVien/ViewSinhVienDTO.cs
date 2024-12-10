using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Result_Management_System.DTOs.SinhVien
{
    public class ViewSinhVienDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Ten { get; set; } = string.Empty;
        [Required]
        public int KhoaId { get; set; }
        [Required]
        public int NamBatDau { get; set; }
    }
}