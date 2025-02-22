using System;

namespace Student_Result_Management_System.Models;

public class GiangVien
{
    public int Id { get; set; }
    public string MaGiangVien { get; set; } = string.Empty;
    public int? KhoaId { get; set; }
    public Khoa? Khoa { get; set; }
    public List<LopHocPhan> LopHocPhans { get; set; } = [];
    public int? TaiKhoanId { get; set; }
    public TaiKhoan? TaiKhoan { get; set; } = new TaiKhoan();
}
