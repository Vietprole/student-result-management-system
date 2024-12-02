using System;

namespace Student_Result_Management_System.Models;

public class GiangVien
{
    public int Id { get; set; }
    public int? KhoaId { get; set; }
    public Khoa? Khoa { get; set; }
    public List<LopHocPhan> LopHocPhans { get; set; } = [];
    public string TaiKhoanId { get; set; } = string.Empty;
    public TaiKhoan TaiKhoan { get; set; } = new TaiKhoan();
}
