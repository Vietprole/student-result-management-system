using System;
using Student_Result_Management_System.DTOs.SinhVien;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.DTOs.LopHocPhan;

public class LopHocPhanDTO
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public int HocPhanId { get; set; }
    public int HocKyId { get; set; }
}
