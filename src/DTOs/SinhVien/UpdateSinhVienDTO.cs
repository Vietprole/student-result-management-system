using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.SinhVien;

public class UpdateSinhVienDTO
{
    [Required]
    public string Ten { get; set; } = string.Empty;
}
