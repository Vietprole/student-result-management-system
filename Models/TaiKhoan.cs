using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Result_Management_System.Models
{
    public class TaiKhoan
    {
        public int Id {get; set;}
        public string TenDangNhap {get; set;} = string.Empty;
        public string MatKhau {get; set;} = string.Empty;
        public int ChucVuId {get; set;}
        public Boolean TrangThai {get; set;} = true;
        public ChucVu? ChucVu {get; set;}
   
    }
}