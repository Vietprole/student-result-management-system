using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.CLO;

public class CreateCLODTO
{
    [Required]
    [MinLength(1)]
    public string Ten { get; set; } = string.Empty;
    [Required]
    [MinLength(1)]
    public string MoTa { get; set; } = string.Empty;
    [Required]
    public int LopHocPhanId { get; set; }
}
