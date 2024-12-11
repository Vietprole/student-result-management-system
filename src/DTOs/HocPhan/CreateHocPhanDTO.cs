using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Result_Management_System.DTOs.HocPhan;

public class CreateHocPhanDTO
{
    [Required]
    [MinLength(1)]
    public string Ten { get; set; } = string.Empty;
    [Required]
    [Column(TypeName = "decimal(3, 1)")]
    [Range(0.0, double.MaxValue, ErrorMessage = "Số tín chỉ phải lớn hơn hoặc bằng 0")]
    public decimal SoTinChi { get; set; }
    [Required]
    public bool LaCotLoi { get; set; }
    [Required]
    public int KhoaId { get; set; }
}
