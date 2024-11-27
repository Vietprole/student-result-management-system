using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Result_Management_System.Models;

public class BaiKiemTra
{
    public int Id { get; set; }
    public string Loai { get; set; } = string.Empty;
    [Column(TypeName = "decimal(4, 2)")]
    public decimal TrongSo { get; set; }
    public int LopHocPhanId { get; set; }
    public LopHocPhan LopHocPhan { get; set; } = null!;
}
