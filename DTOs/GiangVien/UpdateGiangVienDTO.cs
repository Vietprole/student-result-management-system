using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.GiangVien;

public class UpdateGiangVienDTO
{
    [Required]
    public string Ten { get; set; } = string.Empty;
    [Required]
    public int KhoaId { get; set; }
}
