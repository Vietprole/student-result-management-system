using System;

namespace Student_Result_Management_System.Models;

public class CauHoi
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public float TrongSo { get; set; }
    public int BaiKiemTraId { get; set; }
    public BaiKiemTra BaiKiemTra { get; set; } = null!;
    public List<CLO> CLOs { get; set; } = [];
}
