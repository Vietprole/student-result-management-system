using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.TaiKhoan;

public class ChangePasswordDTO
{
    [Required]
    public string OldPassword { get; set; } = null!;
    [Required]
    public string NewPassword { get; set; } = null!;
}
