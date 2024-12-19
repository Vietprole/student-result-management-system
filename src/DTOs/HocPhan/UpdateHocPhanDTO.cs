using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Result_Management_System.DTOs.HocPhan;

public class UpdateHocPhanDTO
{
    public string? Ten { get; set; } = string.Empty;
    [Column(TypeName = "decimal(3, 1)")]
    [Range(0.0, double.MaxValue, ErrorMessage = "Số tín chỉ phải lớn hơn hoặc bằng 0")]
    public decimal? SoTinChi { get; set; }
    public bool? LaCotLoi { get; set; }
}
