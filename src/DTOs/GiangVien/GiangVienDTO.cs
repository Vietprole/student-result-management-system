using System;

namespace Student_Result_Management_System.DTOs.GiangVien;

public class GiangVienDTO
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public int? KhoaId { get; set; }
    public string TenKhoa { get; set; } = string.Empty;
}
