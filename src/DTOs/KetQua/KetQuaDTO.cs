using System;

namespace Student_Result_Management_System.DTOs.KetQua;

public class KetQuaDTO
{
    public int Id { get; set; }
    public decimal DiemTam { get; set; }
    public decimal? DiemChinhThuc { get; set; }
    public bool DaCongBo { get; set; }
    public bool DaXacNhan { get; set; }
    public int SinhVienId { get; set; }
    public int CauHoiId { get; set; }
}
