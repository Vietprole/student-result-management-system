namespace Student_Result_Management_System.Models
{
    public class SinhVien
    {
        public int Id { get; set; }
        public int KhoaNhapHoc { get; set; }
        public int KhoaId { get; set; }
        public Khoa Khoa { get; set; } = null!;
        public List<LopHocPhan> LopHocPhans { get; set; } = [];
        public int? TaiKhoanId { get; set; }
        public TaiKhoan? TaiKhoan { get; set; } =new TaiKhoan();

    }
}
