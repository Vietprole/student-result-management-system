using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.HocPhan;

public class UpdateHocPhanDTO
{
    [Required]
    public string Ten { get; set; } = string.Empty;
    [Required]
    public int SoTinChi { get; set; }
    [Required]
    public bool LaCotLoi { get; set; }
    [Required]
    public int KhoaId { get; set; }
}
