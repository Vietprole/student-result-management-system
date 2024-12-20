using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Result_Management_System.DTOs.HocKy
{
    public class HocKyDTO
    {
        public int Id { get; set; }
        public string Ten { get; set; } = string.Empty;
        public int NamHoc { get; set; }
        public string MaHocKy { get; set; } = string.Empty;
        public string TenHienThi { get; set; } = string.Empty;
    }
}