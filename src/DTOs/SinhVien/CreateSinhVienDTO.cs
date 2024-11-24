using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.SinhVien;

public class CreateSinhVienDTO
{
    [Required]
    public string Ten { get; set; } = string.Empty;
    [Required]
    public string TenKhoa { get; set; } = string.Empty;
    [Required]
    public int NamBatDau { get; set; }
}
