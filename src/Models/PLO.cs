using System;

namespace Student_Result_Management_System.Models;

public class PLO
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public string MoTa { get; set; } = string.Empty;
    public int NganhId { get; set; }
    public Nganh Nganh { get; set; } = null!;
    public List<CLO> CLOs { get; set; } = [];
    public List<HocPhan> HocPhans { get; set; } = [];
}
