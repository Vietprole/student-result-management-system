using System;

namespace Student_Result_Management_System.DTOs.CLO;

public class CLODTO
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public string Mota { get; set; } = string.Empty;
    public int LopHocPhanId { get; set; }
}
