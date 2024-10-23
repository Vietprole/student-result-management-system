using System;

namespace Student_Result_Management_System.Models;

public class Diem
{
    public int Id { get; set; }
    public int SinhVienId { get; set; }
    public SinhVien SinhVien { get; set; } = null!;
}
