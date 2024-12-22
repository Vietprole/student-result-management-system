using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Result_Management_System.DTOs.DiemDinhChinh;

public class UpdateDiemDinhChinhDTO
{
	[Required]
	public int KetQuaId { get; set; }
	[Column(TypeName = "decimal(5, 2)")]
	public decimal? DiemMoi { get; set; }
	public bool? DuocDuyet { get; set; }
}