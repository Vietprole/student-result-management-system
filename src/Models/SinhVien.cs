namespace Student_Result_Management_System.Models
{
    public class SinhVien
    {
        public int Id { get; set; }
        public string MaSinhVien { get; set; } = string.Empty;
        public int NamNhapHoc { get; set; }
        public int KhoaId { get; set; }
        public Khoa Khoa { get; set; } = null!;
        public int? NganhId { get; set; }
        public Nganh? Nganh { get; set; } = null!;
        public List<LopHocPhan> LopHocPhans { get; set; } = [];
        public int? TaiKhoanId { get; set; }
        public TaiKhoan? TaiKhoan { get; set; } = new TaiKhoan();
    }
}
