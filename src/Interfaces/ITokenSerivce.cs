using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Interfaces
{
    public interface ITokenSerivce
    {
        public Task<String> CreateToken(TaiKhoan taikhoan);
        public Task<String> GetFullNameAndRole(string token);
        
    }
}