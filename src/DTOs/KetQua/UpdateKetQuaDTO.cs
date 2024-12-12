using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Result_Management_System.DTOs.KetQua;

public class UpdateKetQuaDTO
{
    [Column(TypeName = "decimal(5, 2)")]
    public decimal? DiemTam { get; set; }
    [Column(TypeName = "decimal(5, 2)")]
    public decimal? DiemChinhThuc { get; set; }
    public bool? DaCongBo { get; set; }
    public bool? DaXacNhan { get; set; }
}
