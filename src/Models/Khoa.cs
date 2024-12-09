using System;

namespace Student_Result_Management_System.Models;

public class Khoa
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public string MaKhoa { get; set; } = string.Empty;
    public int? TruongKhoaId { get; set; }
    public TaiKhoan? TruongKhoa { get; set; }
    public List<HocPhan> HocPhans { get; set; } = [];
    public List<GiangVien> GiangViens { get; set; } = [];
    public List<Nganh> Nganhs { get; set; } = [];
    public List<SinhVien> SinhViens { get; set; } = [];
}
