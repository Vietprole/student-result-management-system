using System;

namespace Student_Result_Management_System.Models;

public class PLO
{
    public int Id { get; set; }
    public List<CLO> CLOs { get; set; } = [];
    public List<HocPhan> HocPhans { get; set; } = [];
}
