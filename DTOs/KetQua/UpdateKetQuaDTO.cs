using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.KetQua;

public class UpdateKetQuaDTO
{
    [Required]
    public int SinhVienId { get; set; }
    [Required]
    public int CauHoiId { get; set; }
    [Required]
    public float Diem { get; set; }
}
