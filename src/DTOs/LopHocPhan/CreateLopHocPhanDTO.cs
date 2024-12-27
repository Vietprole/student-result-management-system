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
    [Required]
    public DateTime HanDeXuatCongThucDiem { get; set; }
    [Required]
    public int GiangVienId { get; set; }
}
