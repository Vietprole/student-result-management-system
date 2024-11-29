using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Result_Management_System.DTOs.SinhVien
{
    public class SinhVienLoginDTO
    {
            public int Id { get; set; }
            public string Ten { get; set; } = string.Empty;
            public int? TaiKhoanId { get; set; }
    }
}