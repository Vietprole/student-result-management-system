using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Result_Management_System.DTOs.DiemDinhChinh;

public class CreateDiemDinhChinhDTO
{
	public int KetQuaId { get; set; }
	[Column(TypeName = "decimal(5, 2)")]
	public decimal DiemMoi { get; set; }
	public DateTime ThoiGian { get; set; }
	public bool DuocDuyet { get; set; }
	public int? NguoiDuyetId { get; set; }
}