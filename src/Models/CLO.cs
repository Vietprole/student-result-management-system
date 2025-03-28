namespace Student_Result_Management_System.Models;

public class CLO
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public string MoTa { get; set; } = string.Empty;
    public int HocPhanId { get; set; }
    public HocPhan HocPhan { get; set; } = null!;
    public int HocKyId { get; set; }
    public HocKy HocKy { get; set; } = null!;
    public List<LopHocPhan> LopHocPhans { get; set; } = [];
    public List<PLO> PLOs { get; set; } = [];
    public List<CauHoi> CauHois { get; set; } = [];
}
