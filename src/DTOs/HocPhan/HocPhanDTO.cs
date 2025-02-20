using System;

namespace Student_Result_Management_System.DTOs.HocPhan;

public class HocPhanDTO
{
    public int Id { get; set; }
    public string MaHocPhan { get; set; } = string.Empty;
    public string Ten { get; set; } = string.Empty;
    public decimal SoTinChi { get; set; }
    public int KhoaId { get; set; }
    public string TenKhoa { get; set; } = string.Empty;
    public bool? LaCotLoi { get; set; } = false;
}
