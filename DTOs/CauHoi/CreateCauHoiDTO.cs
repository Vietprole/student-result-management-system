using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.CauHoi;

public class CreateCauHoiDTO
{   
    [Required]
    public string Ten { get; set; } = string.Empty;
    [Required]
    public float TrongSo { get; set; }
    [Required]
    public int BaiKiemTraId { get; set; }
}
