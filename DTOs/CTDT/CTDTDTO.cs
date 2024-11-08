using System;

namespace Student_Result_Management_System.DTOs.CTDT;

public class CTDTDTO
{
    public int Id { get; set; }
    public string Ten { get; set; } = string.Empty;
    public int? KhoaId { get; set; }
    public int? NganhId { get; set; }
}
