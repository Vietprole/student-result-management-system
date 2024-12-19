using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.LopHocPhan;

public class UpdateLopHocPhanDTO
{
    public string? Ten { get; set; }
    public int? HocPhanId { get; set; }
    public int? HocKyId { get; set; }
    public DateTime? HanDeXuatCongThucDiem { get; set; }
    public int? GiangVienId { get; set; }
}
