using System;

namespace Student_Result_Management_System.Models;

public class Khoa
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public List<CTDT> CTDTs { get; set; } = [];
    public List<HocPhan> HocPhans { get; set; } = [];
}
