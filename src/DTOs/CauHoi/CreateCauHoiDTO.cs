using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Result_Management_System.DTOs.CauHoi;

public class CreateCauHoiDTO
{   
    [Required(ErrorMessage = "Tên câu hỏi không được để trống")]
    public string Ten { get; set; } = string.Empty;
    [Required(ErrorMessage = "Trọng số không được để trống")]
    [Range(0.0, 10.0, ErrorMessage = "Trọng số phải nằm trong khoảng từ 0 đến 10")]
    [Column(TypeName = "decimal(5, 2)")]
    public decimal TrongSo { get; set; }
    public int BaiKiemTraId { get; set; }
    [Required(ErrorMessage = "Thang điểm không được để trống")]
    [Range(0.0, 10.0, ErrorMessage = "Thang điểm phải nằm trong khoảng từ 0 đến 10")]
    [Column(TypeName = "decimal(5, 2)")]
    public decimal ThangDiem { get; set; }
}
