using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace Student_Result_Management_System.DTOs.LopHocPhan;

public class CreateLopHocPhanDTO
{
    [Required(ErrorMessage = "Tên lớp học phần không được để trống")]
    public string Ten { get; set; } = string.Empty;
    [Required]
    public int HocPhanId { get; set; }
    [Required]
    public int HocKyId { get; set; }
    public DateTime HanDeXuatCongThucDiem { get; set; } = DateTime.Now;
    [Required]
    public int GiangVienId { get; set; }
    [Required]
    public string Khoa { get; set; } = string.Empty;
    [Required]
    public string Nhom { get; set; } = string.Empty;
}
