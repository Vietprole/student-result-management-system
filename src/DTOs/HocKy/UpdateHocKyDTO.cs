using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.HocKy;

public class UpdateHocKyDTO
{
    public string? Ten { get; set; } = string.Empty;
    [Range(1900, 9999, ErrorMessage = "Năm học phải từ 1900 đến 9999")]
    public int? NamHoc { get; set; }
    public string? MaHocKy { get; set; } = string.Empty;
}
