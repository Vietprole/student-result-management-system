using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Result_Management_System.Models
{
    public class KiHoc
    {
        public int Id { get; set; }
        public string Ten { get; set; } = string.Empty;
        public string NamHoc { get; set; } = string.Empty;
        public DateTime? HanSuaDiem { get; set; }
        public DateTime? HanSuaCongThucDiem { get; set; }
        public List<LopHocPhan> lopHocPhans=new List<LopHocPhan>();
    }
}