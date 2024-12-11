using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.CLO;

public class UpdateCLODTO
{
    [MinLength(1)]
    public string? Ten { get; set; } = string.Empty;
    [MinLength(1)]
    public string? MoTa { get; set; } = string.Empty;
    public int? LopHocPhanId { get; set; }
}
