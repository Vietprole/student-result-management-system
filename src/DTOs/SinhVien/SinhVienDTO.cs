namespace Student_Result_Management_System.DTOs.SinhVien;

public class SinhVienDTO
{
    public int Id { get; set; }
    public string MaSinhVien { get; set; } = string.Empty;
    public string Ten { get; set; } = string.Empty;
    public int KhoaId { get; set; }
    public string TenKhoa { get; set; } = string.Empty;
    public int NamNhapHoc { get; set; }
}
