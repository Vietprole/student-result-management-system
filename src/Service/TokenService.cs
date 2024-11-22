using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Service
{
    public class TokenService : ITokenSerivce
    {
        private readonly UserManager<TaiKhoan> _userManager;
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config,UserManager<TaiKhoan> userManager)
        {
            _config = config;
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"] ?? throw new ArgumentNullException("JWT:SigningKey")));

        }
        public Task<string> CreateToken(TaiKhoan taikhoan)
        {
            throw new NotImplementedException();
        }
    }
}