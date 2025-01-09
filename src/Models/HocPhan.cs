using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Result_Management_System.Models;

public class HocPhan
{
    public int Id { get; set; }
    public string MaHocPhan { get; set; } = string.Empty;
    public string Ten { get; set; } = string.Empty;
    [Column(TypeName = "decimal(3, 1)")]
    public decimal SoTinChi { get; set; }
    public int KhoaId { get; set; }
    public Khoa Khoa { get; set; } = null!;
    // public List<Nganh> Nganhs { get; set; } = [];
    public List<LopHocPhan> LopHocPhans { get; set; } = [];
    public List<PLO> PLOs { get; set; } = [];
    // public List<Ctdt> Ctdts { get; set; } = [];
}
