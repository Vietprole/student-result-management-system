using System;

namespace Student_Result_Management_System.DTOs.CauHoi;

public class CauHoiDTO
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public decimal TrongSo { get; set; }
    public int BaiKiemTraId { get; set; }
    public decimal ThangDiem { get; set; }
}
