using System;

namespace Student_Result_Management_System.Models;

public class Khoa
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public ICollection<CTDT> CTDTs { get; set; } = new List<CTDT>();
}
