using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.PLO;

public class CreatePLODTO
{
    [Required(ErrorMessage = "Tên PLO không được để trống")]
    public string Ten { get; set; } = string.Empty;
    [Required(ErrorMessage = "Mô tả không được để trống")]
    public string MoTa { get; set; } = string.Empty;
    [Required]
    public int NganhId { get; set; }
}
