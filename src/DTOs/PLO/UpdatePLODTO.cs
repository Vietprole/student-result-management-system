using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.PLO;

public class UpdatePLODTO
{
    public string? Ten { get; set; } = string.Empty;
    public string? MoTa { get; set; } = string.Empty;
    public int? NganhId { get; set; }
}
