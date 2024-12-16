using System.ComponentModel.DataAnnotations;

namespace Student_Result_Management_System.DTOs.ChucVu
{
    public class ChucVuDTO
    {
        [Required]
        public string TenChucVu { get; set; } = string.Empty;
    }
}