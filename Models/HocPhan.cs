using System;

namespace Student_Result_Management_System.Models;

public class HocPhan
{
    public int Id { get; set; }
    public int IdKhoa { get; set; }
    public string Ten { get; set; } = string.Empty;
    public int SoTinChi { get; set; }
    public bool LaCotLoi { get; set; }
}
