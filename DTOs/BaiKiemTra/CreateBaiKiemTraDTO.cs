using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.BaiKiemTra;

public class CreateBaiKiemTraDTO
{
    [Required]
    public string Loai { get; set; } = string.Empty;
    [Required]
    public float TrongSo { get; set; }
    [Required]
    public int LopHocPhanId { get; set; }
}
