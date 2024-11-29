using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Result_Management_System.Models
{
    public class PhanQuyen
    {
        public int Id {get; set;}
        public string TenQuyen {get; set;} = string.Empty;
        public int ChucVuId {get; set;}
        public ChucVu? ChucVu {get; set;}
    }
}