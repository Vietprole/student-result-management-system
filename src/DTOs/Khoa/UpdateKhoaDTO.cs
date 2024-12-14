using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.Khoa;

public class UpdateKhoaDTO
{
    public string? Ten { get; set; } = string.Empty;
    public string? MaKhoa { get; set; } = string.Empty;
}
