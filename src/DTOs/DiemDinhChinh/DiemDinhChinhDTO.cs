namespace Student_Result_Management_System.DTOs.DiemDinhChinh;

public class DiemDinhChinhDTO
{
	public int Id { get; set; }
	public int SinhVienId { get; set; }
	public string MaSinhVien { get; set; } = string.Empty;
	public string TenSinhVien { get; set; } = string.Empty;
	public string LoaiBaiKiemTra { get; set; } = string.Empty;
	public DateTime? HanDinhChinh { get; set; }
	public int CauHoiId { get; set; }
	public string TenCauHoi { get; set; } = string.Empty;
	public decimal? DiemCu { get; set; }
	public decimal DiemMoi { get; set; }
	public decimal TrongSo { get; set; }
	public decimal ThangDiem { get; set; }
	public DateTime ThoiDiemMo { get; set; }
	public DateTime? ThoiDiemDuyet { get; set; }
	public bool DuocDuyet { get; set; }
	public int? NguoiDuyetId { get; set; }
    public string? TenNguoiDuyet { get; set; }
}