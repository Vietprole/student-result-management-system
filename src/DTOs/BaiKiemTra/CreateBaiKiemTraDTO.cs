using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Result_Management_System.DTOs.BaiKiemTra;

public class CreateBaiKiemTraDTO
{
    [Required(ErrorMessage = "Loại không được để trống")]
    public string Loai { get; set; } = string.Empty;
    [Range(0.0, 1.0, ErrorMessage = "Trọng số phải nằm trong khoảng từ 0 đến 1")]
    [Column(TypeName = "decimal(5,2)")]
    public decimal? TrongSo { get; set; }
    [Range(0.0, 1.0, ErrorMessage = "Trọng số phải nằm trong khoảng từ 0 đến 1")]
    [Column(TypeName = "decimal(5,2)")]
    public decimal? TrongSoDeXuat { get; set; }
    public DateTime? NgayMoNhapDiem { get; set; }
    public DateTime? HanNhapDiem { get; set; }
    public DateTime? HanDinhChinh { get; set; }
    public DateTime? NgayXacNhan { get; set; }
    public int LopHocPhanId { get; set; }
}
