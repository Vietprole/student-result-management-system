using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Result_Management_System.Models;

public class KetQua
{
    public int Id { get; set; }
    [Column(TypeName = "decimal(5, 2)")]
    public decimal DiemTam{ get; set; }
    [Column(TypeName = "decimal(5, 2)")]
    public decimal DiemChinhThuc { get; set; }
    public bool DaCongBo { get; set; }
    public bool DaXacNhan { get; set; }
    public int SinhVienId { get; set; }
    public SinhVien SinhVien { get; set; } = null!;
    public int CauHoiId { get; set; }
    public CauHoi CauHoi { get; set; } = null!;
}
