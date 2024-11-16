using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Result_Management_System.Models
{
    public class ChucVu
    {
        public int Id {get; set;}
        public string TenChucVu {get; set;} = string.Empty;
        public List<PhanQuyen> PhanQuyens {get; set;} = [];
    }
}