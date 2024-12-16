using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.Khoa;

public class CreateKhoaDTO
{
    [Required(ErrorMessage = "Tên khoa không được để trống")]
    public string Ten { get; set; } = string.Empty;
    [Required(ErrorMessage = "Mã khoa không được để trống")]
    public string MaKhoa { get; set; } = string.Empty;
}
