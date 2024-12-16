using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.GiangVien;

public class CreateGiangVienDTO
{
    public string Ten { get; set; } = string.Empty;
    public int KhoaId { get; set; }
}
