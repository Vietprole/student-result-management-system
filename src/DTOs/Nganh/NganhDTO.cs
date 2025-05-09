using System;

namespace Student_Result_Management_System.DTOs.Nganh;

public class NganhDTO
{
    public int Id { get; set; }
    public string MaNganh { get; set; } = string.Empty;
    public string Ten { get; set; } = string.Empty;
    public int KhoaId { get; set; }
    public string TenKhoa { get; set; } = string.Empty;
    public int? NguoiQuanLyId { get; set; } = null;
    public string? TenNguoiQuanLy { get; set; } = null;
}
