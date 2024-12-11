using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.HocKy;

public class UpdateHocKyDTO
{
    [MinLength(1)]
    public string? Ten { get; set; } = string.Empty;
    [Range(0, 9999)]
    public int? NamHoc { get; set; }
    [MinLength(1)]
    public string? MaHocKy { get; set; } = string.Empty;
}
