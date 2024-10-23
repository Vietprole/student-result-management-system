using System;

namespace Student_Result_Management_System.Models;

public class GiangVien
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public int KhoaId { get; set; }
    public Khoa? Khoa { get; set; }
}
