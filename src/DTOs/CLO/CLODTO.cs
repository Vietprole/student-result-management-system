namespace Student_Result_Management_System.DTOs.CLO;

public class CLODTO
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public string MoTa { get; set; } = string.Empty;
    public int HocPhanId { get; set; }
    public string TenHocPhan { get; set; } = string.Empty;
    public int? HocKyId { get; set; }
    public string? TenHocKy { get; set; } = string.Empty;
}
