using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.Nganh;

public class CreateNganhDTO
{
    [Required(ErrorMessage = "Tên ngành không được để trống")]
    public string Ten { get; set; } = string.Empty;
    [Required]
    public int KhoaId { get; set; }
    public int[]? HocPhanId { get; set; }
}
