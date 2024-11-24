using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.Khoa;

public class CreateKhoaDTO
{
    [Required]
    public string Ten { get; set; } = string.Empty;
    [Required]
    public string MaKhoa { get; set; } = string.Empty;
    [Required]
    public string VietTat { get; set; } = string.Empty;
}
