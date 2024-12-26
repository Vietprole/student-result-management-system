using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.SinhVien;

public class UpdateSinhVienDTO
{
    public string? Ten { get; set; } = string.Empty;
    public int? KhoaId { get; set; }
    public int? NganhId { get; set; }
    public int? NamNhapHoc { get; set; }
}
