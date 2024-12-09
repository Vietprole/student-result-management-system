using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.DiemDinhChinh;

public class DiemDinhChinhDTO
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public int KhoaId { get; set; }
}