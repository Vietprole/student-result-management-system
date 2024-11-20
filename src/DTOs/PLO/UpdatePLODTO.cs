using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.PLO;

public class UpdatePLODTO
{
    [Required]
    public string Ten { get; set; } = string.Empty;
    public string MoTa { get; set; } = string.Empty;
    [Required]
    public int CTDTId { get; set; }
}
