using System;
using Student_Result_Management_System.Services;

namespace Student_Result_Management_System.Models;

public class Nganh
{
    public int Id { get; set; }
    public string MaNganh { get; set; } = string.Empty;
    public string Ten { get; set; } = string.Empty;
    public int KhoaId { get; set; }
    public Khoa Khoa { get; set; } = null!;
    public int? TaiKhoanId { get; set; }
    public TaiKhoan? TaiKhoan { get; set; }
    public List<HocPhan> HocPhans { get; set; } = [];
    public List<PLO> PLOs { get; set; } = [];
    public List<Ctdt> Ctdts { get; set; } = [];
}
