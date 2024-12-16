namespace Student_Result_Management_System.DTOs.BaiKiemTra;

public class BaiKiemTraDTO
{
    public int Id { get; set; }
    public string Loai { get; set; } = string.Empty;
    public decimal? TrongSo { get; set; }
    public decimal? TrongSoDeXuat { get; set; }
    public DateTime? NgayMoNhapDiem { get; set; }
    public DateTime? HanNhapDiem { get; set; }
    public DateTime? HanDinhChinh { get; set; }
    public DateTime? NgayXacNhan { get; set; }
    public int LopHocPhanId { get; set; }
}
