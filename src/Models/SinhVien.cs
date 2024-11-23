﻿namespace Student_Result_Management_System.Models
{
    public class SinhVien
    {
        public int Id { get; set; }
        public string Ten { get; set; } = string.Empty;
        public List<LopHocPhan> LopHocPhans { get; set; } = [];
        public string TaiKhoanId { get; set; } = string.Empty;
        public TaiKhoan TaiKhoan { get; set; } =new TaiKhoan();
    }
}