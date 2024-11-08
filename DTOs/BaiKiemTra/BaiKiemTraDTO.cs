using System;

namespace Student_Result_Management_System.DTOs.BaiKiemTra;

public class BaiKiemTraDTO
{
    public int Id { get; set; }
    public string Loai { get; set; } = string.Empty;
    public float TrongSo { get; set; }
    public int LopHocPhanId { get; set; }
}
