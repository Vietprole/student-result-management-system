using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.PLO;

public class CreatePLODTO
{
    [Required]
    [MinLength(1)]
    public string Ten { get; set; } = string.Empty;
    [Required]
    [MinLength(1)]
    public string MoTa { get; set; } = string.Empty;
    [Required]
    public int NganhId { get; set; }
}
