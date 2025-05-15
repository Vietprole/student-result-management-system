using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.CLO;

public class CreateCLODTO
{
    [Required(ErrorMessage = "Tên CLO không được để trống")]
    public string Ten { get; set; } = string.Empty;
    [Required(ErrorMessage = "Mô tả không được để trống")]
    public string MoTa { get; set; } = string.Empty;
    [Required]
    public int HocPhanId { get; set; }
    public int? HocKyId { get; set; }
}
