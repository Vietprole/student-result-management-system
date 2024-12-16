using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Antiforgery;

namespace Student_Result_Management_System.DTOs.DiemDinhChinh;

public class CreateDiemDinhChinhDTO
{
	[Required]
	public int KetQuaId { get; set; }
	[Required(ErrorMessage = "Điểm mới không được để trống")]
	[Column(TypeName = "decimal(5, 2)")]
	public decimal DiemMoi { get; set; }
	public DateTime ThoiDiemMo { get; set; } = DateTime.Now;
	public DateTime? ThoiDiemDuyet { get; set; }
	public bool DuocDuyet { get; set; }
	public int? NguoiDuyetId { get; set; }
}