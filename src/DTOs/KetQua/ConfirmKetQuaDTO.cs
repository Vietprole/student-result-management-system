using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.KetQua;

public class ConfirmKetQuaDTO
{
    [Required(ErrorMessage = "Mã sinh viên không được để trống")]
    public int SinhVienId { get; set; }
    [Required(ErrorMessage = "Mã câu hỏi không được để trống")]
    public int CauHoiId { get; set; }
}
