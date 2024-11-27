using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.KetQua;

public class CreateKetQuaDTO
{
    [Required]
    public decimal Diem { get; set; }
    [Required]
    public int SinhVienId { get; set; }
    [Required]
    public int CauHoiId { get; set; }
}
