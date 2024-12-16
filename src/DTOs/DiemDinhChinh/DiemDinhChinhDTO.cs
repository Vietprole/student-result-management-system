namespace Student_Result_Management_System.DTOs.DiemDinhChinh;

public class DiemDinhChinhDTO
{
	public int Id { get; set; }
	public int KetQuaId { get; set; }
	public decimal DiemMoi { get; set; }
	public DateTime ThoiDiemMo { get; set; }
	public DateTime? ThoiDiemDuyet { get; set; }
	public bool DuocDuyet { get; set; }
	public int? NguoiDuyetId { get; set; }
}