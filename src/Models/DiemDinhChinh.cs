using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Result_Management_System.Models;

public class DiemDinhChinh
{
	public int Id { get; set; }
	public int SinhVienId { get; set; }
	public SinhVien SinhVien { get; set; } = null!;
	public int CauHoiId { get; set; }
	public CauHoi CauHoi { get; set; } = null!;
	[Column(TypeName = "decimal(5, 2)")]
	public decimal DiemMoi { get; set; }
	public DateTime ThoiDiemMo { get; set; }
	public DateTime? ThoiDiemDuyet { get; set; }
	public bool DuocDuyet { get; set; }
	public int? NguoiDuyetId { get; set; }
	public TaiKhoan? NguoiDuyet { get; set; } = null!;
}
