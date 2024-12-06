using System;
using System.ComponentModel.DataAnnotations;
using Student_Result_Management_System.DTOs.CauHoi;

namespace Student_Result_Management_System.DTOs.BaiKiemTra;

public class CreateBaiKiemTraDTO
{
    [Required]
    public string Loai { get; set; } = string.Empty;
    [Required]
    public decimal TrongSo { get; set; }
    [Required]
    public int LopHocPhanId { get; set; }

}
