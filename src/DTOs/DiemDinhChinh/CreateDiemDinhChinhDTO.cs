using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.DiemDinhChinh;

public class CreateDiemDinhChinhDTO
{
    [Required]
    public string Ten { get; set; } = string.Empty;
    [Required]
    public int KhoaId { get; set; }
}