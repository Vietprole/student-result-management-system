using System;

namespace Student_Result_Management_System.Models;

public class CTDT
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public int SoTinChi { get; set; }
    public int? IdKhoa { get; set; }
    public int? IdNganh { get; set; }
    public Khoa? Khoa { get; set; }
    public Nganh? Nganh { get; set; }
    public ICollection<HocPhan> HocPhans { get; set; } = [];
}
