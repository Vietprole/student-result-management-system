using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Result_Management_System.Models;

public class DiemDinhChinh
{
	public int Id { get; set; }
	public int KetQuaId { get; set; }
	public KetQua KetQua { get; set; } = null!;
	[Column(TypeName = "decimal(5, 2)")]
	public decimal DiemMoi { get; set; }
	public DateTime ThoiGian { get; set; }
	public bool DuocDuyet { get; set; }
	public int? NguoiDuyetId { get; set; }
}
