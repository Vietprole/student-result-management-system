using System;

namespace Student_Result_Management_System.Models;

public class CLO
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public string Mota { get; set; } = string.Empty;
    public int LopHocPhanId { get; set; }
    public List<PLO> PLOs { get; set; } = [];
    public List<CauHoi> CauHois { get; set; } = [];
}
