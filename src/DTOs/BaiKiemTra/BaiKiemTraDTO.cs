using System;
using Student_Result_Management_System.DTOs.CauHoi;

namespace Student_Result_Management_System.DTOs.BaiKiemTra;

public class BaiKiemTraDTO
{
    public int Id { get; set; }
    public string Loai { get; set; } = string.Empty;
    public decimal TrongSo { get; set; }
    public int LopHocPhanId { get; set; }
    public string TenNguoiTao { get; set; } = string.Empty;
    public DateTime NgayTao { get; set; }
    public List<CauHoiDTO> CauHois { get; set; } = [];
}
