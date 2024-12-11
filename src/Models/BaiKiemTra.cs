using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Result_Management_System.Models;

public class BaiKiemTra
{
    public int Id { get; set; }
    public string Loai { get; set; } = string.Empty;
    [Column(TypeName = "decimal(5, 2)")]
    public decimal? TrongSo { get; set; }
    [Column(TypeName = "decimal(5, 2)")]
    public decimal? TrongSoDeXuat { get; set; }
    public DateTime? NgayMoNhapDiem { get; set; }
    public DateTime? HanNhapDiem { get; set; }
    public DateTime? HanDinhChinh { get; set; }
    public DateTime? NgayXacNhan { get; set; }
    public int LopHocPhanId { get; set; }
    public LopHocPhan LopHocPhan { get; set; } = null!;
    public List<CauHoi> CauHois { get; set; } = [];
}
