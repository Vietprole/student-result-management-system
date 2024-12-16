using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.LopHocPhan;

public class UpdateLopHocPhanDTO
{
    public string? Ten { get; set; } = string.Empty;
    public int? HocPhanId { get; set; }
    public int? HocKyId { get; set; }
}
