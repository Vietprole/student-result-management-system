using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.SinhVien;

public class CreateSinhVienDTO
{
    [Required(ErrorMessage = "Tên sinh viên không được để trống")]
    public string Ten { get; set; } = string.Empty;
    [Required(ErrorMessage = "Năm nhập học không được để trống")]
    public int NamNhapHoc { get; set; }
    public int KhoaId { get; set; }
    public int? NganhId { get; set; }
}
