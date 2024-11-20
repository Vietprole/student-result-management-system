using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.CTDT;

public class CreateCTDTDTO
{
    [Required]
    public string Ten { get; set; } = string.Empty;
    [Required]
    public int NganhId { get; set; }
}
