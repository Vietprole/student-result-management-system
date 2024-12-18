using System;

namespace Student_Result_Management_System.Models;

public class CTDT
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public int? NganhId { get; set; }
    public Nganh? Nganh { get; set; }
    public List<HocPhan> HocPhans { get; set; } = [];
    public List<PLO> PLOs { get; set; } = [];
}
