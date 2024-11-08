using System;

namespace Student_Result_Management_System.Models;

public class BaiKiemTra
{
    public int Id { get; set; }
    public string Loai { get; set; } = string.Empty;
    public float TrongSo { get; set; }
    public int LopHocPhanId { get; set; }
    public LopHocPhan LopHocPhan { get; set; } = null!;
}
