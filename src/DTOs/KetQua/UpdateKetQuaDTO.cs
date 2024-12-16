using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Result_Management_System.DTOs.KetQua;

public class UpdateKetQuaDTO
{
    [Required(ErrorMessage = "Điểm tạm không được để trống")]
    [Column(TypeName = "decimal(5, 2)")]
    public decimal DiemTam{ get; set; }
    [Column(TypeName = "decimal(5, 2)")]
    public decimal? DiemChinhThuc { get; set; }
    public bool DaCongBo { get; set; }
    public bool DaXacNhan { get; set; }
    [Required(ErrorMessage = "Sinh viên Id không được để trống")]
    public int SinhVienId { get; set; }
    [Required(ErrorMessage = "Câu hỏi Id không được để trống")]
    public int CauHoiId { get; set; }
}
