using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Result_Management_System.DTOs.TaiKhoan
{
    public class NewTaiKhoanDTO
    {
        public required string Username {get; set;}
        public required string Token {get; set;}
    }
}