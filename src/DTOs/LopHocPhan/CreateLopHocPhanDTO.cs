using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.LopHocPhan;

public class CreateLopHocPhanDTO
{
    [Required]
    public string Ten { get; set; } = string.Empty;
    [Required]
    public int HocPhanId { get; set; }
}
