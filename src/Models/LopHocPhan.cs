using System;
using System.Runtime.InteropServices;

namespace Student_Result_Management_System.Models;

public class LopHocPhan
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public string MaLopHocPhan { get; set; } = string.Empty;
    public int HocPhanId { get; set; }
    public HocPhan HocPhan { get; set; } = null!;
    public int HocKyId { get; set; }
    public HocKy HocKy { get; set; } = new HocKy();
    public DateTime HanDeXuatCongThucDiem { get; set; }
    public int? NguoiDeXuatCongThucDiemId { get; set; }
    public int? NguoiChapNhanCongThucDiemId { get; set; }
    public DateTime? NgayChapNhanCongThucDiem { get; set; }
    public List<SinhVien> SinhViens { get; set; } = [];
    public List<CLO> CLOs { get; set; } = [];
    public int GiangVienId { get; set; }
    public GiangVien GiangVien { get; set; } = null!;
}
