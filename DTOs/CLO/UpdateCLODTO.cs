using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.CLO;

public class UpdateCLODTO
{
    [Required]
    public string Ten { get; set; } = string.Empty;
    [Required]
    public string Mota { get; set; } = string.Empty;
    [Required]
    public int LopHocPhanId { get; set; }
}
