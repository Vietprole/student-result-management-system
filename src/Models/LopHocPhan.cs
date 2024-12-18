using System;
using System.Runtime.InteropServices;

namespace Student_Result_Management_System.Models;

public class LopHocPhan
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public int HocPhanId { get; set; }
    public HocPhan HocPhan { get; set; } = null!;
    public List<SinhVien> SinhViens { get; set; } = [];
    public List<CLO> CLOs { get; set; } = [];
    public List<GiangVien> GiangViens { get; set; } = [];
}
