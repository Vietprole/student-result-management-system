using System;

namespace Student_Result_Management_System.Models;

public class Nganh
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public int? KhoaId { get; set; }
    public Khoa? Khoa { get; set; }
    public List<CTDT> CTDTs { get; set; } = [];
    public List<LopHocPhan> LopHocPhans { get; set; } = [];
    public int? TaiKhoanId { get; set; }
    public TaiKhoan? TaiKhoan { get; set; }
}
