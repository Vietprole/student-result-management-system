namespace Student_Result_Management_System.DTOs.LopHocPhan
{
    public class LopHocPhanChiTietDTO
    {
        public string MaLopHocPhan { get; set; } = string.Empty;
        public string TenLopHocPhan { get; set; } = string.Empty;
        public string TenGiangVien { get; set; } = string.Empty;
        public int SoLuongSinhVien { get; set; }
        public int NamHoc { get; set; }
        public string TenHocKy { get; set; } = string.Empty;
    }
}
