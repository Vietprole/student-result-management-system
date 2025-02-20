using System;
using Student_Result_Management_System.DTOs.SinhVien;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.DTOs.LopHocPhan;

public class LopHocPhanDTO
{
    public int Id { get; set; }
    public string MaLopHocPhan { get; set; } = string.Empty;
    public string Ten { get; set; } = string.Empty;
    public int HocPhanId { get; set; }
    public string TenHocPhan { get; set; } = string.Empty;
    public int HocKyId { get; set; }
    public string TenHocKy { get; set; } = string.Empty;
    public DateTime HanDeXuatCongThucDiem { get; set; }
    public int GiangVienId { get; set; }
    public string TenGiangVien { get; set; } = string.Empty;
    public string NamHoc { get; set; } = string.Empty;
}
