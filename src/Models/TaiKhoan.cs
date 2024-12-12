using System;
using System.ComponentModel.DataAnnotations;


namespace Student_Result_Management_System.Models
{
    public class TaiKhoan
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;
        [MaxLength(512)]
        public string Password { get; set; } = string.Empty;
        [MaxLength(512)]
        public string Ten {  get; set; } = string.Empty;
        public int ChucVuId { get; set; }
        public ChucVu ChucVu { get; set; } = new ChucVu();
    }
}