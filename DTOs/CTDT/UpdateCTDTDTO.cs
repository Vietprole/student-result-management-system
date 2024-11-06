using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.CTDT;

public class UpdateCTDTDTO
{
    [Required]
    public string Ten { get; set; } = string.Empty;
    [Required]
    public int KhoaId { get; set; }
    [Required]
    public int NganhId { get; set; }
}
