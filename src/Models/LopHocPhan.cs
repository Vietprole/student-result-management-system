using System;
using System.Runtime.InteropServices;

namespace Student_Result_Management_System.Models;

public class LopHocPhan
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public int HocPhanId { get; set; }
    public HocPhan HocPhan { get; set; } = null!;
    public int KiHocId { get; set; }
    public KiHoc KiHoc { get; set; } = new KiHoc();
    public string? TenNguoiXacNhanCTD { get; set; }
    public DateTime? NgayXacNhanCTD { get; set; }
    public string? TenNguoiChapNhanCTD { get; set; }
    public DateTime? NgayChapNhanCTD { get; set; }
    public List<SinhVien> SinhViens { get; set; } = [];
    public List<CLO> CLOs { get; set; } = [];
    public List<GiangVien> GiangViens { get; set; } = [];
}
