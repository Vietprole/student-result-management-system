using System;

namespace Student_Result_Management_System.DTOs.HocPhan;

public class HocPhanDTO
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public float SoTinChi { get; set; }
    public bool LaCotLoi { get; set; }
    public int? KhoaId { get; set; }
}
