using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Result_Management_System.DTOs.HocPhan;

public class CreateHocPhanDTO
{
    [Required(ErrorMessage = "Tên học phần không được để trống")]
    public string Ten { get; set; } = string.Empty;
    [Required(ErrorMessage = "Số tín chỉ không được để trống")]
    [Column(TypeName = "decimal(3, 1)")]
    [Range(0.0, double.MaxValue, ErrorMessage = "Số tín chỉ phải lớn hơn hoặc bằng 0")]
    public decimal SoTinChi { get; set; }
    [Required]
    public bool LaCotLoi { get; set; }
    [Required]
    public int KhoaId { get; set; }
}
