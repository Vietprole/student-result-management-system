using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.CLO;

public class CLODTO
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Ten { get; set; } = string.Empty;
    public string MoTa { get; set; } = string.Empty;
    [Required]
    public int LopHocPhanId { get; set; }
}
