using System;

namespace Student_Result_Management_System.Models;

public class HocPhan
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public int SoTinChi { get; set; }
    public bool LaCotLoi { get; set; }
    public int? NganhId { get; set; }
    public Nganh? Nganh { get; set; }
    public List<CTDT> CTDTs { get; set; } = [];
    public List<LopHocPhan> LopHocPhans { get; set; } = [];
    public List<PLO> PLOs { get; set; } = [];
}
