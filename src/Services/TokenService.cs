using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Student_Result_Management_System.Data;
using Student_Result_Management_System.Interfaces;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly ApplicationDBContext _context;
        public TokenService(IConfiguration config, ApplicationDBContext context)
        {
            _config = config;
            var signingKey = _config["JWT:SigningKey"];
            if (string.IsNullOrEmpty(signingKey))
            {
                throw new ArgumentNullException("JWT:SigningKey", "Signing key cannot be null or empty.");
            }
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            _context = context;

        }
        public string CreateToken(TaiKhoan user)
        {
            var giangVienId = _context.GiangViens.Where(gv => gv.TaiKhoanId == user.Id).Select(gv => gv.Id).FirstOrDefault();
            var sinhVienId = _context.SinhViens.Where(sv => sv.TaiKhoanId == user.Id).Select(sv => sv.Id).FirstOrDefault();
             var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("userId",user.Id.ToString()),
                new Claim("fullname",user.Ten),
                new Claim(ClaimTypes.Role,user.ChucVu.TenChucVu),
                new Claim("giangVienId", giangVienId.ToString() ?? ""),
                new Claim("sinhVienId", sinhVienId.ToString() ?? "")
            };
            var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public Task<string> GetFullNameAndRole(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
            {
                throw new ArgumentException("Invalid JWT token");
            }
            var jwtToken = handler.ReadJwtToken(token);
            var fullName = jwtToken.Claims.FirstOrDefault(c => c.Type == "fullname")?.Value ?? throw new ArgumentNullException("fullname", "Full name claim cannot be null.");
        
            return Task.FromResult(fullName);
        }
    }
}