using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Result_Management_System.Models
{
    public class HocKy
    {
        public int Id { get; set; }
        public string Ten { get; set; } = string.Empty;
        public int NamHoc { get; set; }
        public string MaHocKy { get; set; } = string.Empty;
        public List<LopHocPhan> lopHocPhans = [];
    }
}