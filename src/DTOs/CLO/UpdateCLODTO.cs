using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.CLO;

public class UpdateCLODTO
{
    public string? Ten { get; set; } = string.Empty;
    public string? MoTa { get; set; } = string.Empty;
    public int? HocPhanId { get; set; }
    public int? HocKyId { get; set; }
}
